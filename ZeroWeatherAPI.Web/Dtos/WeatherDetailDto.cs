namespace ZeroWeatherAPI.Web.Dtos
{
    public class WeatherDetailDto
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public double Weather { get; set; }
        public double ThermalSensation { get; set; }
    }
}
