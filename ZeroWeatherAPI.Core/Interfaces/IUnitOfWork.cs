using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Repositories;

namespace ZeroWeatherAPI.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IWeatherRepository WeatherRepository { get; }

        Task<int> CommitAsync();

        IEnumerable<StoredProcedureDto> StoredProcedureDtoRpt(int id);
    }
}
