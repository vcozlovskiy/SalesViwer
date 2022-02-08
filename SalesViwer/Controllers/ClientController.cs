using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Models;
using SalesViwer.Client.Configuration;
using SalesViwer.Client.ViewsModels;
using SalesViwer.DAL.UoWs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.Controllers
{
    public class ClientController : Controller, IDisposable
    {
        private readonly IOptions<DbConfiguration> config;
        protected BaseUoW<SalesInfoManager.Persistence.Models.Client> clientUoW;

        public ClientController(IOptions<DbConfiguration> config)
        {
            this.config = config;
            clientUoW = new FactoryUoW<SalesInfoManager.Persistence.Models.Client>()
            .CreateInstant(config.Value.ConnectionString);
        }

        public async Task<JsonResult> ClientsJson()
        {
            return await Task.Run(() =>
            {
                var clients = clientUoW.Repository.Include("Orders.Item").ToList();

                return Json(clients);
            });
        }

        public async Task<JsonResult> ClientSortedJson()
        {
            return await Task.Run(() =>
            {
                var clients = clientUoW.Repository.Include("Orders.Item")
                .ToList()
                .OrderBy(c => c.FullName);

                return Json(clients);
            });
        }

        public async Task<IActionResult> Table()
        {
            return await Task.Run(() => View());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Int64 id)
        {
            SalesInfoManager.Persistence.Models.Client client = clientUoW.Repository.Get((int)id);

            var orderItem = clientUoW.Repository.Context
                .Set<SalesInfoManager.Persistence.Models.Client>()
                .Include("Orders.Item")
                .ToList();
            var i = orderItem
                .Where(c => c.Id == id)
                .First()
                .Orders
                .ToList();

            EditClientViewModel edit = new EditClientViewModel()
            {
                Id = (int)id,
                ClientName = client.FullName,
            };

            return View(edit);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Save(SalesInfoManager.Persistence.Models.Client client)
        {
            clientUoW.Repository.Add(client);
            clientUoW.SaveChanges();
            return View("Table");
        }
        public async Task<JsonResult> GetOrderInfo(Int64 id)
        {
            return await Task.Run(() =>
            {
                int f = (int)id;
                var orderItem = clientUoW.Repository.Context
                   .Set<SalesInfoManager.Persistence.Models.Client>()
                   .Include("Orders.Item")
                   .ToList();
                var i = orderItem
                    .Where(c => c.Id == id)
                    .First()
                    .Orders
                    .ToList();

                var j = Json(i);

                return Json(i);
            });

            //< tr >
            //        < td > @order.Item.Name </ td >
            //        < td > @order.Price </ td >
            //        < td > @order.dateTimeOrder </ td >
            //        < td >< button class="btn btn-info btn-sm" onclick="Edit();">Edit</button></td>
            //    </tr>
        }

        protected override void Dispose(bool disposing)
        {
            clientUoW.Dispose();
            base.Dispose(disposing);
        }
    }
}
