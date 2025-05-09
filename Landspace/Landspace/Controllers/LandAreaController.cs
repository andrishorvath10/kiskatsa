using Microsoft.AspNetCore.Mvc;
using Landspace.Models;
using Landspace.Services;

namespace Landspace.Controllers
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
        public ActionResult<LandAreaResponse> CalculateLargestArea([FromBody] LandAreaRequest request)
        {
            if (request == null || request.Matrix == null)
            {
                return BadRequest("A mátrix nem lehet null.");
            }

            int n = request.Matrix.Length;
            for (int i = 0; i < n; i++)
            {
                if (request.Matrix[i] == null || request.Matrix[i].Length != n)
                {
                    return BadRequest("A mátrixnak négyzetes formájúnak (n×n) kell lennie.");
                }
            }

            var result = _landAreaService.CalculateLargestArea(request);

            return Ok(result);
        }
    }
}

