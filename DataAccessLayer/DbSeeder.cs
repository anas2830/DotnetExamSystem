using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DotnetExamSystem.Api.DataAccessLayer
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(MongoDbContext context)
        {
            var usersCollection = context.GetCollection<User>("Users");
            var existingAdmin = await usersCollection.Find(u => u.Role == "Admin").FirstOrDefaultAsync();
            if (existingAdmin != null)
                return;
            var admin = new User
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = Helpers.PasswordHelper.HashPassword("Admin@123"),
                Role = "Admin"
            };

            await usersCollection.InsertOneAsync(admin);
        }
    }
}
