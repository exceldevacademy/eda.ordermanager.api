using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Entities
{
    [Table("CompanyOrder")]
    public class CompanyOrder
    {
        [Key]
        [Required]
        [Column("OrderId")]
        public int CompanyOrderId { get; set; }

        [Column("InternalOrderNo")]
        public string InternalOrderNo { get; set; }

        [Column("ExternalOrderNo")]
        public string ExternalOrderNo { get; set; }

        [Column("VendorId")]
        public int VendorId { get; set; }

        [Column("PurchaseDate")]
        public DateTimeOffset PurchaseDate { get; set; }

        [Column("ArrivalDate")]
        public DateTimeOffset ArrivalDate { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Amount")]
        public int Amount { get; set; }

        [Column("Comments")]
        public string Comments { get; set; }

    }
}
