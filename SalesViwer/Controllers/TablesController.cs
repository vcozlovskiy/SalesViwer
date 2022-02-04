using Microsoft.AspNetCore.Mvc;
using SalesViwer.DAL.UoWs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.Controllers
{
    public class TablesController : Controller
    {
        public async Task<JsonResult> GetInfo()
        {
            return await Task.Run(() =>
            {
                ClientUoW s = new ClientUoW();
                var value = s.GetEntityUoW.Repository.
                Context.Set<SalesInfoManager.Persistence.Models.Client>().ToList();
                var j = Json(value);

                return Json(value);
            });
        }
    }
}
