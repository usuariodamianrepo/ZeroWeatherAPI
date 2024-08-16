using ZeroWeatherAPI.Core.Dtos;

namespace ZeroWeatherAPI.Core.Interfaces.Services
{
    public interface IStoredProcedureService
    {
        IEnumerable<StoredProcedureDto> GetAll(int cityId);
    }
}
