using Microsoft.EntityFrameworkCore;
using ShorterLinks.Server.DTOs;
using ShorterLinks.Server.Repositories;
using ShorterLinks.Server.Repositories.Models;

namespace ShorterLinks.Server.Services
{
    public class LinkService : ILinkService
    {
        private readonly ApplicationDbContext _context;

        public LinkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShortLinkDto> CreateLinkAsync(int userId, string originalUrl)
        {
            if(!Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
            {
                throw new Exception("Некоректный URL");
            }

            var shortCode = await GenetrateUniqueShortCodeAsync();

            var shortLink = new ShortLink
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                ClickCount = 0,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
            };

            await _context.ShortLinks.AddAsync(shortLink);  

            await _context.SaveChangesAsync();
            return new ShortLinkDto
            {
                Id = shortLink.Id,
                OriginalUrl = shortLink.OriginalUrl,
                ShortCode = shortLink.ShortCode,
                ShortUrl = shortLink.ShortUrl,
                ClickCount = shortLink.ClickCount,
                CreatedAt = shortLink.CreatedAt,
            };

        }

        public async Task DeleteLinkAsync(int userId, int linkId)
        {
            var link = await _context.ShortLinks
                .FirstOrDefaultAsync(l => l.Id == linkId && l.UserId == userId);

            if (link == null)
            {
                throw new Exception("Ссылка не найдена");
            }

            _context.ShortLinks.Remove(link);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetOriginalUrlAsync(string shortCode)
        {
            var link = await _context.ShortLinks
                .FirstOrDefaultAsync(l => l.ShortCode == shortCode);

            return link?.OriginalUrl ?? string.Empty;
        }

        public async Task<List<ShortLinkDto>> GetUserLinksAsync(int userId)
        {
            var links = await _context.ShortLinks
               .Where(l => l.UserId == userId)
               .OrderByDescending(l => l.CreatedAt)
               .Select(l => new ShortLinkDto
               {
                   Id = l.Id,
                   OriginalUrl = l.OriginalUrl,
                   ShortCode = l.ShortCode,
                   ShortUrl = l.ShortUrl,
                   ClickCount = l.ClickCount,
                   CreatedAt = l.CreatedAt

               }).ToListAsync<ShortLinkDto>()
               ;
            return links;
        }

        public async Task IncrementClickCountAsync(string shortCode)
        {
            var link = await _context.ShortLinks
                .FirstOrDefaultAsync(l => l.ShortCode == shortCode);

            if (link != null)
            {
                link.ClickCount++;
                link.LastAccessed = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        private async Task<string> GenetrateUniqueShortCodeAsync()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string shortCode;

            do {
                shortCode = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            } while (await _context.ShortLinks.AnyAsync(l => l.ShortCode == shortCode));
        
            return shortCode;
        }

        public async Task<LinkStatsDto> GetLinkStatsAsync(int userId, int linkId)
        {
            var link = await _context.ShortLinks
                .FirstOrDefaultAsync(l => l.Id == linkId && l.UserId == userId);

            if (link == null)
            {
                throw new Exception("Ссылка не найдена");
            }

            return new LinkStatsDto
            {
                Id = link.Id,
                OriginalUrl = link.OriginalUrl,
                ShortUrl = link.ShortUrl,
                ClickCount = link.ClickCount,
                CreatedAt = link.CreatedAt,
                LastAccessedAt = link.LastAccessed,
            };
        }
    }
}
