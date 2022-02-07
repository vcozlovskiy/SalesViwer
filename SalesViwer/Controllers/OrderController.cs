using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Models;
using SalesViwer.Client.ViewsModels;
using SalesViwer.DAL.UoWs;
using SalesViwer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.Controllers
{
    public class OrderController : Controller, IDisposable
    {
        protected BaseUoW<SalesInfoManager.Persistence.Models.Client> clientUoW = new FactoryUoW<SalesInfoManager.Persistence.Models.Client>().CreateInstant();
        protected BaseUoW<Item> itemUow = new FactoryUoW<Item>().CreateInstant();
        protected BaseUoW<Order> orderUoW = new FactoryUoW<Order>().CreateInstant();
        protected BaseUoW<Manager> managerUoW = new FactoryUoW<Manager>().CreateInstant();
        protected int ClientId;
        private bool isDisposed = false;

        public async Task<IActionResult> Add(Int64 id)
        {
            int clientIn = (int)id;

            return await Task.Run(() => View(new AddOrderViewModel(clientIn)
            {
                dateTimeOrder = DateTime.Now,
            }));
        }
        public Task<IActionResult> Edit()
        {
            return null;
        }
        public async Task<JsonResult> JsonOrders()
        {
            return await Task.Run(() =>
            {
                var ordes = orderUoW.Repository.Include("Item").ToList();

                return Json(ordes);
            });
        }
        public async Task<IActionResult> Save(AddOrderViewModel newOrderModel)
        {
            return await Task.Run(() =>
            {
                Order newOrder = new Order()
                {
                    dateTimeOrder = newOrderModel.dateTimeOrder,
                    Item = itemUow.Repository.Get((i) => i.Name == newOrderModel.ItemName).FirstOrDefault(),
                    Price = newOrderModel.Price,
                };

                if (newOrder.Item == null)
                {
                    newOrder.Item = new Item()
                    {
                        Name = newOrderModel.ItemName,
                    };
                }

                SalesInfoManager.Persistence.Models.Client client =
                clientUoW.Repository
                .Get(c => c.Id == newOrderModel.ClientId)
                .First();

                Manager manager = managerUoW.Repository
                .Get(m => m.FullName == newOrderModel.ManagerFullName)
                .FirstOrDefault();

                if (manager == null)
                {
                    manager = new Manager()
                    {
                        FullName = newOrderModel.ManagerFullName,
                    };
                    managerUoW.Repository.Add(manager);
                }

                manager.Orders.Add(newOrder);
                client.Orders.Add(newOrder);

                clientUoW.SaveChanges();
                managerUoW.SaveChanges();

                return View("~/Views/Tables/Table.cshtml");
            });
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed) return;

            if (isDisposing)
            {
                if (itemUow != null)
                {
                    itemUow.Dispose();
                    itemUow = null;
                }
                if (clientUoW != null)
                {
                    clientUoW.Dispose();
                    clientUoW = null;
                }
            }
            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }
        ~OrderController()
        {
            Dispose(false);
        }
    }
}
