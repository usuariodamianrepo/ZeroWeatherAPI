using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Infrastructure.Data;

namespace ZeroWeatherAPI.Infrastructure.Repositories
{
    public class StoredProcedureRepository : BaseRepository<StoredProcedureDto>, IStoredProcedureRepository
    {
        private const string SQL_PARAMETER_PREFIX = "@";

        public StoredProcedureRepository(AppDbContext context) : base(context)
        {

        }

        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(object[] sqlParameters) where TEntity : class, IStoredProcedure, new()
        {
            return _context.Set<TEntity>().FromSqlRaw<TEntity>((new TEntity()).Query, sqlParameters).ToList();
        }

        public SqlParameter GetSqlParameter(string name, object value, bool isOutput = false)
        {
            if (!name.StartsWith(SQL_PARAMETER_PREFIX))
            {
                name += SQL_PARAMETER_PREFIX;
            }

            var direction = isOutput ? System.Data.ParameterDirection.Output : System.Data.ParameterDirection.Input;

            return new SqlParameter
            {
                ParameterName = name,
                Value = value,
                Direction = direction
            };
        }
    }
}