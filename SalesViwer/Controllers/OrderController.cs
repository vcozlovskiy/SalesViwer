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
    public class OrderController : Controller
    {
        protected AddEntityUoW<SalesInfoManager.Persistence.Models.Client> addEntity = new FactoryUoW<SalesInfoManager.Persistence.Models.Client>().CreateInstantsAdd();
        protected GetEntityUoW<Item> itemUow = new FactoryUoW<Item>().CreateInstantsGet();
        protected int ClientId;
        public Task<IActionResult> Edit()
        {
            return null;
        }

        public async Task<IActionResult> Add(Int64 id)
        {
            int clientIn = (int)id;

            return await Task.Run(() => View(new AddOrderViewModel(clientIn) 
            {
                ItemName = "Item",
            }));
        }
        public async Task<IActionResult> Save(AddOrderViewModel newOrderModel)
        {
            return await Task.Run(() =>
            {
                Order newOrder = new Order()
                {
                    dateTimeOrder = newOrderModel.dateTimeOrder,
                    Item = itemUow.GetItem(newOrderModel.ItemName),
                    Price = newOrderModel.Price,
                };

                SalesInfoManager.Persistence.Models.Client client = 
                addEntity.Repository
                .Get(c => c.Id == newOrderModel.ClientId)
                .First();

                client.Orders.Add(newOrder);

                addEntity.SaveChanges();

                return View("~/Views/Tables/Table.cshtml");
            });
        }
    }
}
