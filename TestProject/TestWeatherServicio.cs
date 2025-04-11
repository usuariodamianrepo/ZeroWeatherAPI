using Moq;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Core.Interfaces.Shared;
using ZeroWeatherAPI.Services;
using System.Net.Http.Json;
using System.Net;
using System;
using Moq.Protected;

namespace TestProject
{
    [TestClass]
    public class TestWeatherServicio
    {
        private readonly int _Id = 1;
        private ZeroWeatherAPI.Core.Entities.Weather _weather;
        private IEnumerable<ZeroWeatherAPI.Core.Entities.Weather> _weathers;
        private City _city;
        private ZeroWeatherAPI.Core.Dtos.Root _rootDto;

        [TestInitialize]
        public void TestInitialize()
        {
            _weather = new ZeroWeatherAPI.Core.Entities.Weather()
            {
                Id = _Id,
                InsertDate = DateTime.Now.AddHours(-1),
                UpdateDate = null,
                CityId = 1,
                CoordLon = 12.03,
                CoordLat = 22.01,
                WeatherId = 100,
                WeatherMain = "WeatherMain",
                WeatherDescription = "WeatherDescription",
                WeatherIcon = "WeatherIcon",
                Base = "Base",
                MainTemp = 2.3,
                MainFeelsLike = 3.6,
                MainTempMin = 2.3,
                MainTempMax = 66.3,
                MainPressure = 66,
                MainHumidity = 22,
                MainSeaLevel = 66,
                MainGrndLevel = 0,
                Visibility = 3,
                WindSpeed = 3.3,
                WindDeg = 32,
                WindGust = 66.3,
                CloudsAll = 1,
                Dt = 2,
                SysType = 3,
                SysId = 4,
                SysCountry = "AR",
                SysSunrise = 33,
                SysSunset = 44,
                Timezone = 78,
                OpenWeatherId = 88,
                Name = "Name",
                Cod = 7,
            };

            _weathers = new ZeroWeatherAPI.Core.Entities.Weather[] { _weather };

            _city = new City()
            {
                Id = 22,
                InsertDate = DateTime.Now,
                UpdateDate = null,
                Name = "San Nicolas",
                Description = "description",
                Latitude = 33,
                Longitude = 22,
                Country = "AR"
            };

            _rootDto = new ZeroWeatherAPI.Core.Dtos.Root()
            {
                Coord = new Coord()
                {
                    Lon = -58.3816,
                    Lat = -34.6037
                },
                Weather = [
                        new ZeroWeatherAPI.Core.Dtos.Weather() {
                        Id = 804,
                        Main = "Clouds",
                        Description = "overcast clouds",
                        Icon = "04d"
                    }
                ],
                Base = "stations",
                Main = new Main()
                {
                    Temp = 288.39,
                    FeelsLike = 288.03,
                    TempMin = 288.05,
                    TempMax = 289.22,
                    Pressure = 1015,
                    Humidity = 79,
                    SeaLevel = 1015,
                    GrndLevel = 1014
                },
                Visibility = 10000,
                Wind = new Wind()
                {
                    Speed = 8.05,
                    Deg = 23,
                    Gust = 15.65
                },
                Clouds = new Clouds()
                {
                    All = 100
                },
                Dt = 1722446339,
                Sys = new Sys
                {
                    Type = 2,
                    Id = 2092396,
                    Country = "AR",
                    Sunrise = 1722422859,
                    Sunset = 1722460318
                },
                Timezone = -10800,
                Id = 6693229,
                Name = "San Nicolas",
                Cod = 200
            };
        }

        [TestMethod]
        public void TestGetAndSave()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            Mock<IOpenWeatherService> openWeatherService = new Mock<IOpenWeatherService>();
            Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            unitOfWork.Setup(a => a.WeatherRepository.GetAsync(a => a.Equals(1), null, "", false, 10)).ReturnsAsync(_weathers);
            unitOfWork.Setup(c => c.CityRepository.GetByIdAsync(1)).ReturnsAsync(_city);

            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<Root>(_rootDto)
            };
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                            "SendAsync",
                            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                            ItExpr.IsAny<CancellationToken>())
                        .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            openWeatherService.Setup(w => w.GetWeatherAsync("San Nicolas", "AR")).ReturnsAsync(_rootDto);


            unitOfWork.Setup(w => w.WeatherRepository.AddAsync(_weather));

            WeatherService weatherService = new WeatherService(unitOfWork.Object, openWeatherService.Object);
            var newWeather = weatherService.GetWeatherAndSaveInfo(_Id, 10);
            //var resp = newWeather.Result;
            Assert.IsNotNull(newWeather);
        }
    }
}