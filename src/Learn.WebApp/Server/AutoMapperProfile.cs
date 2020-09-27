﻿using AutoMapper;
using Learn.Server.Shared;
using Learn.WebApp.Shared;
using Learn.WebApp.Shared.CoursePath;

namespace Learn.WebApp.Server
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // request models to business models
            CreateMap<WeatherForecastModel, WeatherForecast>();
            CreateMap<CoursePathPostRequestModel, CoursePath>();
            CreateMap<CoursePathDeleteRequestModel, CoursePath>();

            // business models to response models
            CreateMap<CoursePath, CoursePathResponseModel>();
        }
    }
}