using ZeroWeatherAPI.Core.Interfaces.Repositories;

namespace ZeroWeatherAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IWeatherRepository WeatherRepository { get; }
        IStoredProcedureRepository StoredProcedureRepository { get; }

        Task<int> CommitAsync();
    }
}
