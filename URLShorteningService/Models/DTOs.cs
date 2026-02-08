namespace URLShorteningService.Models
{
    public class UpsertUrlEntityDTO
    {

        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
    }
    public class LoginDTO
    {
        public string Email{ get; set; }
    }
}
