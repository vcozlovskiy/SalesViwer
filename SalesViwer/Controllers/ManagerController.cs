using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Models;
using SalesViwer.DAL.UoWs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesViwer.Client.Configuration;

namespace SalesViwer.Client.Controllers
{
    public class ManagerController : Controller
    {
        protected BaseUoW<Manager> managerUoW;
        private readonly IOptions<DbConfiguration> config;

        public ManagerController(IOptions<DbConfiguration> config)
        {
            this.config = config;
            managerUoW = new FactoryUoW<Manager>().CreateInstant(config.Value.ConnectionString);
        }

        public async Task<JsonResult> JsonManagers()
        {
            return await Task.Run(() =>
            {
                var managers = managerUoW.Repository.Include("Orders").ToList();

                return Json(managers);
            });
        }

        public async Task<JsonResult> JsonManagersSortByName()
        {
            return await Task.Run(() =>
            {
                var managers = managerUoW.Repository.Include("Orders").OrderBy(o => o.FullName).ToList();

                return Json(managers);
            });
        }

        public async Task<JsonResult> JsonManagersSortByCountOrders()
        {
            return await Task.Run(() =>
            {
                var sortedOrders = managerUoW.Repository
                .Include("Orders")
                .OrderBy(o => o.Orders.Count()).ToList();

                return Json(sortedOrders);
            });
        }
    }
}
