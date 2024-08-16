using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Services;

namespace ZeroWeatherAPI.Services
{
    public class StoredProcedureService : IStoredProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoredProcedureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<StoredProcedureDto> GetAll(int cityId)
        {
            var stored = _unitOfWork.StoredProcedureRepository;

            var parameters = new object[1];
            parameters[0] = stored.GetSqlParameter("@cityId", cityId);

            IList<StoredProcedureDto> queryResults = stored.ExecuteStoredProcedure<StoredProcedureDto>(parameters).ToList();

            return queryResults;
        }
    }
}
