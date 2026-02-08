using URLShorteningService.Models;

namespace URLShorteningService.Services
{
    public interface IUrlShortenerService
    {
        Task<UrlEntity> SaveUrlEntity(UpsertUrlEntityDTO urlEntity, string apiKey);
        Task<UrlEntity> UpdateUrlEntity(int id, UpsertUrlEntityDTO urlEntity, string apiKey);
        Task<string> ReturnUrlEntity(string shortUrl, HttpContext httpContext);
        Task<List<UrlEntityClick>> ReturnUrlEntityClicks(int id);
        Task<List<UrlEntity>> ReturnUrlEntities(string apikey);

    }
}
