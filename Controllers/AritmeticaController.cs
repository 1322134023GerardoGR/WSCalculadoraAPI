using Microsoft.AspNetCore.Mvc;
using WSCalculadoraAPI.Data;
using WSCalculadoraAPI.Models;

namespace WSCalculadoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AritmeticaController : ControllerBase
    {
        private readonly CalculadoraContext _context;

        public AritmeticaController(CalculadoraContext context)
        {
            _context = context;
        }

        [HttpGet("Sumar")]
        public async Task<ActionResult<double>> Sumar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 + num2;
            await GuardarOperacion(num1, num2, "suma", resultado);
            return resultado;
        }

        [HttpGet("Restar")]
        public async Task<ActionResult<double>> Restar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 - num2;
            await GuardarOperacion(num1, num2, "resta", resultado);
            return resultado;
        }

        [HttpGet("Multiplicar")]
        public async Task<ActionResult<double>> Multiplicar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 * num2;
            await GuardarOperacion(num1, num2, "multiplicacion", resultado);
            return resultado;
        }

        [HttpGet("Dividir")]
        public async Task<ActionResult<double>> Dividir([FromQuery] double num1, [FromQuery] double num2)
        {
            if (num2 == 0) return BadRequest("No se puede dividir por cero.");

            var resultado = num1 / num2;
            await GuardarOperacion(num1, num2, "division", resultado);
            return resultado;
        }

        private async Task GuardarOperacion(double num1, double num2, string operacion, double resultado)
        {
            var registro = new Aritmetica
            {
                Num1 = num1,
                Num2 = num2,
                Operacion = operacion,
                Resultado = resultado
            };

            _context.Calculadora.Add(registro);
            await _context.SaveChangesAsync();
        }
    }
}