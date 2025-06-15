using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WSCalculadoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AritmeticaController : ControllerBase
    {
        [HttpGet("Sumar")]
        public ActionResult<double> Sumar([FromQuery] double num1, [FromQuery] double num2)
        {
            return num1 + num2;
        }

        [HttpGet("Restar")]
        public ActionResult<double> Restar([FromQuery] double num1, [FromQuery] double num2)
        {
            return num1 - num2;
        }

        [HttpGet("Multiplicar")]
        public ActionResult<double> Multiplicar([FromQuery] double num1, [FromQuery] double num2)
        {
            return num1 * num2;
        }

        [HttpGet("Dividir")]
        public ActionResult<double> Dividir([FromQuery] double num1, [FromQuery] double num2)
        {
            if (num2 == 0) return BadRequest("No se puede dividir por cero.");
            return num1 / num2;
        }
    }
}
