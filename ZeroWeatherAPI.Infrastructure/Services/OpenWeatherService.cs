using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Shared;

namespace ZeroWeatherAPI.Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        public UrlSettings _urlSettings { get; }
        private readonly HttpClient _httpClient;

        public OpenWeatherService(IOptions<UrlSettings> urlSettings, HttpClient httpClient)
        {
            _urlSettings = urlSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<Root> GetWeatherAsync(decimal latitude, decimal longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(_urlSettings.OpenWeatherApiKey))
                    throw new Exception("The Api key is null or empty.");

                string url = $"{_httpClient.BaseAddress}?lat={latitude}&lon={longitude}&appid={_urlSettings.OpenWeatherApiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Root rootObject = JsonConvert.DeserializeObject<Root>(responseBody);

                return rootObject;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error when it gets Request from OpenWeatherUrl. {ex.Message}");
            }
        }

        public async Task<Root> GetWeatherAsync(string city, string country)
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                    throw new Exception("The city is null or empty.");
                
                if (string.IsNullOrEmpty(country))
                    throw new Exception("The country is null or empty.");

                if (string.IsNullOrEmpty(_urlSettings.OpenWeatherApiKey))
                    throw new Exception("The Api key is null or empty.");

                string url = $"{_httpClient.BaseAddress}?q={city},{country}&appid={_urlSettings.OpenWeatherApiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Root rootObject = JsonConvert.DeserializeObject<Root>(responseBody);

                return rootObject;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error when it gets Request from OpenWeatherUrl. {ex.Message}");
            }
        }
    }
}