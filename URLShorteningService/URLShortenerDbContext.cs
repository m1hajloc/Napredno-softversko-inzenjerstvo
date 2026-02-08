using Microsoft.EntityFrameworkCore;
using URLShorteningService.Models;

namespace URLShorteningService;

public class URLShortenerDbContext : DbContext
{
    public URLShortenerDbContext(DbContextOptions<URLShortenerDbContext> options)
     : base(options) { }

    public DbSet<UrlEntity> urlEntities => Set<UrlEntity>();
    public DbSet<User> users => Set<User>();
    public DbSet<UrlEntityClick> UrlEntityClicks => Set<UrlEntityClick>();
    
}
