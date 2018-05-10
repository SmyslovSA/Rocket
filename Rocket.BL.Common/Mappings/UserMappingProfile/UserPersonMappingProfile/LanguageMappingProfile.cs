﻿using AutoMapper;
using Rocket.BL.Common.Models.User.Person.Localization;
using Rocket.DAL.Common.DbModels.DbUser.DbPerson;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserPersonMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели языка пользователя с моделью хранения языка пользователя
    /// </summary>
    public class LanguageMappingProfile : Profile
    {
        public LanguageMappingProfile()
        {
            CreateMap<Language, DbLanguage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}