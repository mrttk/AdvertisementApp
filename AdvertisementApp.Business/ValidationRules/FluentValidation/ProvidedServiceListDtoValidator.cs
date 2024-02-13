using AdvertisementApp.Dtos.ProvidedServiceDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation
{
    internal class ProvidedServiceListDtoValidator : AbstractValidator<ProvidedServiceListDto>
    {
        public ProvidedServiceListDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImagePath).NotEmpty();
        }
    }
}
