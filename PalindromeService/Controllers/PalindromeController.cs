using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalindromeServices.Services;

namespace PalindromeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalindromeController(IPalindromeService palindromeService) : ControllerBase
    {
        private readonly IPalindromeService _palindromeService = palindromeService;

        [HttpGet]
        public async Task<IActionResult> IsPalindrome(string text)
        {
            try
            {
                var isPalindrome = await _palindromeService.IsPalindrome(text);
                return Ok(isPalindrome);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
