using URLShorteningService.Models;

namespace URLShorteningService.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByApiKey(string apiKey);
    }
}
