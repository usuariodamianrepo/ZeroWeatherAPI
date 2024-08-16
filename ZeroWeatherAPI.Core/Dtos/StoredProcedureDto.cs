using ZeroWeatherAPI.Core.Interfaces;

namespace ZeroWeatherAPI.Core.Dtos
{
    public class StoredProcedureDto : IStoredProcedure
    {
        public string Query => "[dbo].[sp_GetWeathersByCity] @CityId";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
