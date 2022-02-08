using SalesInfoManager.DAL.Repositories;
using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Context;
using SalesInfoManager.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesViwer.DAL.UoWs
{
    public class FactoryUoW<Entity> where Entity : class
    {
        public BaseUoW<Entity> CreateInstant(string connectionString)
        {
            SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory connection = 
                new SalesInfoManager.BL.ConnectionFactory.
                SqlConnectionFactory(connectionString);

            GenericRepository<Entity> clientRepository = 
                new GenericRepository<Entity>(
                    new SalesInfoManagerDbContext(connection.CreateInstance(), true));

            return new BaseUoW<Entity>(clientRepository);
        }
    }
}
