using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        //simulação, depois implementar a bagaça do repositorio
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new() { Id = 1, UserName = "batman", Password = "batman", Role = "manager" },
                new() { Id = 2, UserName = "robin", Password = "robin", Role = "employee" }
            };

            return users.FirstOrDefault(u => u.UserName.ToLower().Equals(username) && u.Password.ToLower().Equals(password));
        }

        public async Task<User> AuthenticateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
