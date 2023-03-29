using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;
using Luiza.Labs.Infra.Data.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Luiza.Labs.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly LuizaLabsContext _context;
        public UserRepository(
            LuizaLabsContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user) => await _context.Users.InsertOneAsync(user);

        public async Task<User> AuthenticateAsync(LoginVM loginVM)
        {
           var userDb = await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.EmailAddress.Equals(loginVM.EmailAddress) && u.Password.Equals(loginVM.Password));
           return userDb;
        }
    }
}
