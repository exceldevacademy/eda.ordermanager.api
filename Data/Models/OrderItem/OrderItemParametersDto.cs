using eda.ordermanager.api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Models.OrderItem
{
    public class OrderItemParametersDto : PaginationParameters
    {
        public string ExternalOrderNo { get; set; }

        public string QueryString { get; set; }
    }
}
