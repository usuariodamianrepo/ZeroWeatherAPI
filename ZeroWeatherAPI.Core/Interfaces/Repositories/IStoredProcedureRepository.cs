using Microsoft.Data.SqlClient;

namespace ZeroWeatherAPI.Core.Interfaces.Repositories
{
    public interface IStoredProcedureRepository
    {
        IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(object[] sqlParameters) where TEntity : class, IStoredProcedure, new();
        SqlParameter GetSqlParameter(string name, object value, bool isOutput = false);
    }
}
