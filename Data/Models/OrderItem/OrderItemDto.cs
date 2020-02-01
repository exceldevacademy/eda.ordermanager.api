using eda.ordermanager.api.Data.Models.Category;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Models.OrderItem
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public int? Amount { get; set; }
        public string Comments { get; set; }
        public CompanyOrderDto Order { get; set; } = new CompanyOrderDto { };
        public CategoryDto Category { get; set; } = new CategoryDto { };
    }
}
