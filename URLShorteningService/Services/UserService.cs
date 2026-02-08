using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using URLShorteningService.Models;

namespace URLShorteningService.Services
{
    public class UserService : IUserService
    {
        URLShortenerDbContext context;
        public UserService(URLShortenerDbContext context)
        {
            this.context = context;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var existingUser = await this.context.users.FirstOrDefaultAsync(user => user.Email == email);
            if (existingUser != null)
                return existingUser;

            var apiKey = GenerateApiKey();
            var user = new User(email, apiKey);

            context.users.Add(user);
            await context.SaveChangesAsync();
            return user;

        }

        public async Task<User> GetUserByApiKey(string apiKey)
        {
            var user = await this.context.users.FirstOrDefaultAsync(user=>user.ApiKey == apiKey);
            if (user == null)
                throw new Exception("User not found.");
            return user;
        }

        //public async Task<string> GetUserApiKey(string email)
        //{
        //    var user = await this.context.users.FirstOrDefaultAsync(user => user.Email == email);
        //    if (user == null)
        //        throw new Exception("User not found.");
        //    return user.ApiKey;
        //}
        private string GenerateApiKey()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

    }
}
