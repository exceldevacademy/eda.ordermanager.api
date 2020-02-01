using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Models.OrderItem
{
    public abstract class OrderItemForManipulationDto
    {
        public int? OrderId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public int? Amount { get; set; }
        public string Comments { get; set; }
    }
}
