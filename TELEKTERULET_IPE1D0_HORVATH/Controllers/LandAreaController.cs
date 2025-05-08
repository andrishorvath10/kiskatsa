using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TELEKTERULET_IPE1D0_HORVATH.Models;
using TELEKTERULET_IPE1D0_HORVATH.Service;

namespace TELEKTERULET_IPE1D0_HORVATH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandAreaController : ControllerBase
    {
        private readonly LandAreaService _landAreaService;
        public LandAreaController(LandAreaService landAreaService)
        {
            _landAreaService = landAreaService;
        }
        [HttpPost("calculate")]
        public ActionResult<MatrixResponse> CalculateLargestArea([FromBody] MatrixRequest matrixRequest)
        {
            if (matrixRequest==null || matrixRequest.Matrix==null)
            {
                return BadRequest("Invalid input data.");
            }
            for (int i = 0; i < matrixRequest.Matrix.Length; i++)
            {
                if (matrixRequest.Matrix[i] ==null || matrixRequest.Matrix[i].Length!=matrixRequest.Matrix.Length)
                {
                    return BadRequest("Matrix needs to be n*n");
                }
            }
            var result = _landAreaService.CalculateLargestArea(matrixRequest);
            return Ok(result);
        }
    }
}
