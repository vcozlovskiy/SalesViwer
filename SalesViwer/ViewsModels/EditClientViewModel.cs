using SalesInfoManager.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.ViewsModels
{
    public class EditClientViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public EditClientViewModel()
        {
            Orders = new List<Order>();
        }
    }
}
