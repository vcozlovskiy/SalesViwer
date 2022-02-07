using SalesInfoManager.DAL.UoWs;
using SalesInfoManager.Persistence.Models;
using SalesViwer.DAL.UoWs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesViwer.Client.Controllers
{
    public class ManagerController : Controller
    {
        protected BaseUoW<Manager> managerUoW = new FactoryUoW<Manager>().CreateInstant();
        public async Task<JsonResult> JsonManagers()
        {
            return await Task.Run(() =>
            {
                var ordes = managerUoW.Repository.Include("Orders").ToList();

                return Json(ordes);
            });
        }
    }
}
