using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LuizaLabsContext _context;
        public OrderRepository(
            LuizaLabsContext context)
        {
            _context = context;
        }
    }
}
