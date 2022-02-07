using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesViwer.Client.ViewsModels
{
    public class AddOrderViewModel
    {
        public int ClientId { get; set; }
        public string ItemName { get; set; }
        public DateTime dateTimeOrder { get; set; }
        public int Price { get; set; }

        public AddOrderViewModel()
        {
            
        }
        public AddOrderViewModel(int id)
        {
            ClientId = id;
        }
    }
}
