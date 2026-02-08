using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace URLShorteningService.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string ApiKey { get; set; } = null!;
        [JsonIgnore]
        public List<UrlEntity> ShortUrls { get; set; } = new List<UrlEntity>();
        public User()
        {
            
        }

        public User(string email, string apiKey)
        {
            Email = email;
            ApiKey = apiKey;
        }
    }

}
