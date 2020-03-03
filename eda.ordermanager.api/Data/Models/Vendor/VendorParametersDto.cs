using eda.ordermanager.api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Models.Vendor
{
    public class VendorParametersDto : PaginationParameters
    {
        public string VendorName { get; set; }
        public string SortOrder { get; set; }
    }
}
