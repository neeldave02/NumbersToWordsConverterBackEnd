using Microsoft.AspNetCore.Mvc;
using NumbersToWords;
using System.Numerics;

namespace NumbersToWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumbersToWordsController : ControllerBase
    {
        private readonly NumberToWordsConverter converter;

        public NumbersToWordsController()
        {
            converter = new NumberToWordsConverter();
        }

        [HttpGet("convert")]
        public IActionResult ConvertNumberToWords([FromQuery] string number)
        {
            try
            {
                // Try parse small number - for decimals
                if (decimal.TryParse(number, out decimal decimalNumber))
                {
                    string result = converter.ConvertToWords(decimalNumber);
                    return Content(result, "text/plain");
                }
                // Do parsing for big integers
                else if (BigInteger.TryParse(number, out BigInteger bigIntegerNumber))
                {
                    string result = converter.ConvertToWords(bigIntegerNumber);
                    return Content(result, "text/plain");
                }
                else
                {
                    return BadRequest("The input number is invalid.");
                }
            }
            catch (OverflowException)
            {
                return BadRequest("The input number is too large to handle due to overflow.");
            }
        }
    }
}
