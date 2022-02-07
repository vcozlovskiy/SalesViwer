using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Models;
using SalesViwer.Client.ViewsModels;
using SalesViwer.DAL.UoWs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.Controllers
{
    public class TablesController : Controller, IDisposable
    {
        protected ClientUoW clientUoW = new ClientUoW();
        protected AddEntityUoW<SalesInfoManager.Persistence.Models.Client> addEntity =
            new FactoryUoW<SalesInfoManager.Persistence.Models.Client>().CreateInstantsAdd();
        public async Task<JsonResult> GetInfo()
        {
            return await Task.Run(() =>
            {
                var clients = clientUoW.GetClientsInclude().ToList();

                return Json(clients);
            });
        }
        public async Task<IActionResult> Table()
        {
            return await Task.Run(() => View());
        }
        public IActionResult Edit(Int64 id)
        {
            SalesInfoManager.Persistence.Models.Client client = clientUoW.GetEntityUoW.Repository.Get((int)id);

            var orderItem = clientUoW.GetEntityUoW.Repository.Context
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

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(SalesInfoManager.Persistence.Models.Client client)
        {
            addEntity.AddEntity(client);
            addEntity.SaveChanges();
            return View("Table");
        }

        public async Task<JsonResult> GetOrderInfo(Int64 id)
        {
            return await Task.Run(() =>
            {
                int f = (int)id;
                var orderItem = clientUoW.GetEntityUoW.Repository.Context
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
            clientUoW.GetEntityUoW.Dispose();
            addEntity.Dispose();
            base.Dispose(disposing);
        }
    }
}
