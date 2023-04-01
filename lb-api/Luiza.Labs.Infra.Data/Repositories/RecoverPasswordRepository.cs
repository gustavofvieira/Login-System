using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;
using Luiza.Labs.Infra.Data.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Luiza.Labs.Infra.Data.Repositories
{
    public class RecoverPasswordRepository : IRecoverPasswordRepository
    {

        private readonly LuizaLabsContext _context;
        public RecoverPasswordRepository(
            LuizaLabsContext context)
        {
            _context = context;
        }

        public async Task Add(RecoverPassword recoverPassword) => await _context.RecoverPasswords.InsertOneAsync(recoverPassword);

        public async Task<RecoverPassword> GetById(Guid id) => await _context.RecoverPasswords.AsQueryable().Where(r => r.RecoverPasswordId.Equals(id)).FirstOrDefaultAsync();
    }
}
