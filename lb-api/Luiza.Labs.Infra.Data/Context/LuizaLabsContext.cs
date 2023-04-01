using Luiza.Labs.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Infra.Data.Context
{
    public class LuizaLabsContext
    {
        public LuizaLabsContext(IMongoDatabase database) => Database = database;

        public IMongoDatabase Database { get; private set; }

        public IMongoCollection<User> Users => Database.GetCollection<User>("Users");
        public IMongoCollection<RecoverPassword> RecoverPasswords => Database.GetCollection<RecoverPassword>("RecoverPasswords");
    }
}
