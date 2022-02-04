using SalesInfoManager.DAL.Repositories;
using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Context;
using SalesInfoManager.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesInfoManager.BL.ConnectionFactory;

namespace SalesViwer.DAL.UoWs
{
    public class ClientUoW
    {
        public GetEntityUoW<Client> GetEntityUoW; 
        public ClientUoW()
        {
            SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory connection = new SalesInfoManager.BL.ConnectionFactory.SqlConnectionFactory(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sails;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            GenericRepository<Client> clientRepository = new GenericRepository<Client>(new SalesInfoManagerDbContext(connection.CreateInstance(), true));

            GetEntityUoW = new GetEntityUoW<Client>(clientRepository);
        }
    }
}
