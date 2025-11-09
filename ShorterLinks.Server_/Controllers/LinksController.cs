using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShorterLinks.Server.DTOs;
using ShorterLinks.Server.Services;
using System.Security.Claims;

namespace ShorterLinks.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinksController(ILinkService linkService) {
            _linkService = linkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLinks()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var links = await _linkService.GetUserLinksAsync(userId);
            return Ok(links);

        }

        [HttpPost]
        public async Task<IActionResult> CreateLink(CreateLinkDto createLinkDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var link = await _linkService.CreateLinkAsync(userId, createLinkDto.OriginalUrl);
            return Ok(link);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _linkService.DeleteLinkAsync(userId, id);
            return NoContent();
        }

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetLinkStats(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var stats = await _linkService.GetLinkStatsAsync(userId, id);
            return Ok(stats);
        }
    }
}
