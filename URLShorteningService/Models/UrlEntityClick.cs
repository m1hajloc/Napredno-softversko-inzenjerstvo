using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShorteningService.Models
{
    [Table("UrlEntityClick")]
    public class UrlEntityClick
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UrlEntity))]
        public int UrlEntityId { get; set; }
        public UrlEntity UrlEntity { get; set; } = null!;

        public DateTime ClickedAt { get; set; } = DateTime.UtcNow;

        public string? UserAgent { get; set; }
        public string? IpAddress { get; set; }

        public UrlEntityClick()
        {
            
        }
        public UrlEntityClick(int urlEntityId, string userAgent, string IpAddress)
        {
            this.UrlEntityId = urlEntityId;
            this.UserAgent = userAgent;
            this.IpAddress = IpAddress;
        }
    }
    
}
