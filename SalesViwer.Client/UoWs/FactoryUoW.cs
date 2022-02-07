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
        public AddEntityUoW<Entity> CreateInstantsAdd()
        {
            SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory connection = new SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sails;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            GenericRepository<Entity> clientRepository = new GenericRepository<Entity>(new SalesInfoManagerDbContext(connection.CreateInstance(), true));

            return new AddEntityUoW<Entity>(clientRepository);
        }

        public GetEntityUoW<Entity> CreateInstantsGet()
        {
            SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory connection = new SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sails;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            GenericRepository<Entity> clientRepository = new GenericRepository<Entity>(new SalesInfoManagerDbContext(connection.CreateInstance(), true));

            return new GetEntityUoW<Entity>(clientRepository);
        }
    }
}
