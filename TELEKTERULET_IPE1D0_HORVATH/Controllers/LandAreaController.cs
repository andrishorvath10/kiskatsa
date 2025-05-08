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
                for (int j = 0; j < matrixRequest.Matrix[i].Length; j++)
                {
                    Console.Write(matrixRequest.Matrix[i][j] + "\t");
                }
                Console.WriteLine();
            }
            int n = matrixRequest.Matrix.Length;
            for (int i = 0; i < n; i++)
            {
                if (matrixRequest.Matrix[i] == null || matrixRequest.Matrix[i].Length != n)
                {
                    return BadRequest("A mátrixnak négyzetes formájúnak (n×n) kell lennie.");
                }
            }
            var result = _landAreaService.CalculateLargestArea(matrixRequest);
            return Ok(result);
        }
    }
}
