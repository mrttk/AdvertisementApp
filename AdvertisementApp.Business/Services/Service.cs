using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Inferfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.Interfaces;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IValidator<ListDto> _listDtoValidator;
        private readonly IUow _uow;

        public Service(IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IValidator<ListDto> listDtoValidator, IMapper mapper, IUow uow)
        {
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _listDtoValidator = listDtoValidator;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().CreateAsync(createdEntity);
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{id} id'sine sahip data bulunamadı.");
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{id} id'sine sahip data bulunamadı.");
            _uow.GetRepository<T>().Remove(data);
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<T>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} id'sine sahip data bulunamadı.");
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(entity, unchangedData);
                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
        }
    }
}
