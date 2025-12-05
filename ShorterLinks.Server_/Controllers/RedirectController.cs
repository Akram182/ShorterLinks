
using Microsoft.AspNetCore.Mvc;
using ShorterLinks.Server.DTOs;
using ShorterLinks.Server.Services; 

namespace ShorterLinks.Server.Controllers
{
    [ApiController]
    [Route("m/")]   
    public class RedirectController : ControllerBase
    {
        private readonly ILinkService _linkService;
        public RedirectController(ILinkService linkService) {
            _linkService = linkService;
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToUrl(string shortCode)
        {
            var originalUrl = await _linkService.GetOriginalUrlAsync(shortCode);

            if (string.IsNullOrEmpty(originalUrl)) return NotFound();
            Console.WriteLine(originalUrl);

            await _linkService.IncrementClickCountAsync(shortCode);
<<<<<<< HEAD
            
            return RedirectPermanent(originalUrl);
            //return await Redirect(originalUrl);
            //return Ok(originalUrl);
=======
            return Ok(new { url = originalUrl });
>>>>>>> 91981b3af65b691e761491e617536e50b559050a
        }
    }
}
