using PaymentIntegrationProject.Models;
using System.Collections.Generic;

namespace PaymentIntegrationProject.Services
{
    public class UserService
    {
        private static readonly List<User> Users = new List<User>
        {
            new User { Username = "testuser", Password = "password123" }
        };

        public User Authenticate(string username, string password)
        {
            var user = Users.Find(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
