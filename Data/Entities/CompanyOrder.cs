using Sieve.Attributes;
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
        [Sieve(CanSort = true)]
        public string InternalOrderNo { get; set; }

        [Column("ExternalOrderNo")]
        [Sieve(CanSort = true)]
        public string ExternalOrderNo { get; set; }

        [Column("VendorId")]
        public int? VendorId { get; set; }

        [Column("PurchaseDate")]
        [Sieve(CanSort = true)]
        public DateTime? PurchaseDate { get; set; }

        [Column("ArrivalDate")]
        [Sieve(CanSort = true)]
        public DateTime? ArrivalDate { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Amount")]
        [Sieve(CanSort = true)]
        public int? Amount { get; set; }

        [Column("Comments")]
        public string Comments { get; set; }

        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; } = new Vendor { };

    }
}
