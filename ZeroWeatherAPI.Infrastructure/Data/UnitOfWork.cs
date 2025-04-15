using Microsoft.EntityFrameworkCore;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Infrastructure.Repositories;

namespace ZeroWeatherAPI.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private CityRepository _cityRepository;
        private WeatherRepository _weatherRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICityRepository CityRepository => _cityRepository ??= new CityRepository(_context);
        public IWeatherRepository WeatherRepository => _weatherRepository ??= new WeatherRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<StoredProcedureDto> StoredProcedureDtoRpt(int id)
        {
            return _context.Database.SqlQuery<StoredProcedureDto>($"exec sp_GetWeathersByCity @CityId={id}").ToList();
        }
    }
}
