using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Services;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoredProcedureController : ControllerBase
    {
        private readonly IStoredProcedureService _storedProcedureService;

        public StoredProcedureController(IStoredProcedureService storedProcedureService)
        {
            _storedProcedureService = storedProcedureService;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StoredProcedureDto>> Get(int id)
        {
            var result = _storedProcedureService.GetById(id);

            return Ok(result);
        }
    }
}
