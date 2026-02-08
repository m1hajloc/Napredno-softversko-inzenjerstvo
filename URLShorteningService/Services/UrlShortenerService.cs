using Microsoft.EntityFrameworkCore;
using URLShorteningService.Models;

namespace URLShorteningService.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        URLShortenerDbContext context;
        IUserService userService;
        public UrlShortenerService(URLShortenerDbContext dbContext, IUserService userService)
        {
            this.context = dbContext;
            this.userService = userService;
        }
        public async Task<string> ReturnUrlEntity(string shortUrl, HttpContext httpContext)
        {
            var entity = await this.context.urlEntities.FirstOrDefaultAsync(entity => entity.ShortUrl == shortUrl);
            if (entity == null)
                throw new Exception("Url Entity not found.");

            var ip = httpContext.Connection?.RemoteIpAddress?.ToString();
            var localIp = httpContext.Connection?.LocalIpAddress?.ToString();
            var userAgent = httpContext.Request.Headers.UserAgent.ToString();
            var click = new UrlEntityClick(entity.Id, userAgent, ip);
            await this.context.AddAsync(click);
            await this.context.SaveChangesAsync();
            return entity.OriginalUrl;
        }

        public async  Task<UrlEntity> SaveUrlEntity(UpsertUrlEntityDTO urlEntity, string apiKey)
        {
                var user = await userService.GetUserByApiKey(apiKey);
                var originalUrl = NormalizeUrl(urlEntity.OriginalUrl);

                if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
                    throw new Exception("Your URL is not valid.");

                var newEntity = urlEntity.ShortUrl != null ? new UrlEntity(originalUrl, urlEntity.ShortUrl, user.Id) : new UrlEntity(originalUrl, user.Id);

                await this.context.AddAsync<UrlEntity>(newEntity);
                await this.context.SaveChangesAsync();

                if (urlEntity.ShortUrl == null)
                {
                    newEntity.ShortUrl = Base62Encode(newEntity.Id);
                    await this.context.SaveChangesAsync();
                }
                return newEntity;
        }
        public Task<List<UrlEntityClick>> ReturnUrlEntityClicks(int id)
        {
            return this.context.UrlEntityClicks.Where(x => x.UrlEntityId == id).ToListAsync();
        }

        public async Task<List<UrlEntity>> ReturnUrlEntities(string apikey)
        {
            var userId = await userService.GetUserByApiKey(apikey);
            return await this.context.urlEntities.Where(x => x.UserId == userId.Id).ToListAsync();
        }

        private string NormalizeUrl(string url)
        {
            if (url.StartsWith("https://") || url.StartsWith("http://"))
                return url;
            else return "https://" + url;
        }
        private string Base62Encode(long id)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = "";

            while (id > 0)
            {
                result = chars[(int)(id % 62)] + result;
                id /= 62;
            }

            return result;
        }

        public async Task<UrlEntity> UpdateUrlEntity(int id, UpsertUrlEntityDTO urlEntity, string apiKey)
        {
            var entity = await this.context.urlEntities.FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity == null)
                throw new Exception("Url Entity not found.");

            var user = await userService.GetUserByApiKey(apiKey);
            if (user == null )
                throw new Exception("User not found.");

            if (entity.UserId != user.Id)
                throw new Exception("Editing Entities of other owners not allowed.");

            var originalUrl = NormalizeUrl(urlEntity.OriginalUrl);

            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
                throw new Exception("Your URL is not valid.");

            if (entity.OriginalUrl != originalUrl )
                entity.OriginalUrl = originalUrl;
            if (urlEntity.ShortUrl != null && entity.ShortUrl != urlEntity.ShortUrl)
                entity.ShortUrl = urlEntity.ShortUrl;

            //await this.context.urlEntities.Update(entity);
            await this.context.SaveChangesAsync();
            return entity;
        }
    }
}
