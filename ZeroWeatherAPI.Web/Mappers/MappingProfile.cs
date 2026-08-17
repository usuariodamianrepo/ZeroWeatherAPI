using Mapster;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Web.Dtos;

namespace ZeroWeatherAPI.Web.Mappers
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<City, CitySimpleDto>().PreserveReference(true);
            config.NewConfig<City, CityDto>().PreserveReference(true);
            config.NewConfig<Weather, WeatherDto>().PreserveReference(true);
            config.NewConfig<Weather, WeatherDetailDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.CityName, src => src.Name)
                .Map(dest => dest.CountryName, src => src.SysCountry)
                .Map(dest => dest.Weather, src => src.MainTemp)
                .Map(dest => dest.ThermalSensation, src => src.MainFeelsLike);
            config.NewConfig<CitySaveDto, City>();
        }
    }
}
