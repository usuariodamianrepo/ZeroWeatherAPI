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
        
        public IEnumerable<StoredProcedureDto> GetById(int cityId)
        {
            return _unitOfWork.StoredProcedureDtoRpt(cityId);
        }
    }
}
