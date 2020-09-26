using AutoMapper;

namespace Learn.WebApp.Server
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Shared.WeatherForecast, Learn.Server.Shared.WeatherForecast>().ReverseMap();
            CreateMap<Shared.CoursePathApiModel, Learn.Server.Shared.CoursePath>().ReverseMap();
        }
    }
}