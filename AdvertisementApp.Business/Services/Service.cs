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
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IDto, new()
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

        public Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> RemoveAsync<IDto>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
