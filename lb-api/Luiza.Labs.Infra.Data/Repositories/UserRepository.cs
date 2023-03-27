using Luiza.Labs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Infra.Data.Repositories
{
    public class UserRepository
    {
        //simulação, depois implementar a bagaça do repositorio
        public static User Get(string username, string password)
        {
            var users = new List<User>();

            users.Add(new User { Id = 1, UserName = "batman", Password = "batman", Role = "manager" });
            users.Add(new User { Id = 1, UserName = "robin", Password = "robin", Role = "employee" });
            return users.FirstOrDefault(u => u.UserName.ToLower().Equals(username) && u.Password.ToLower().Equals(password));
        }
    }
}
