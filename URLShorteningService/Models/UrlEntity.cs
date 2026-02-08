using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShorteningService.Models;

[Table("UrlEntity")]
public class UrlEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string OriginalUrl { get; set; }
    [Required]
    public string ShortUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsGenerated { get; set; } = true;
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }

    public UrlEntity()
    {
        
    }
    public UrlEntity(string url, int userId)
    {
        this.OriginalUrl = url;
        this.ShortUrl = "a";
        this.UserId = userId;
    }
    public UrlEntity(string originalUrl, string shortUrl, int userId)
    {
        this.OriginalUrl = originalUrl;
        this.ShortUrl = shortUrl;
        this.IsGenerated = false;
        this.UserId = userId;
    }

}
