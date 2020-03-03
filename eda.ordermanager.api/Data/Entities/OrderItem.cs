using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Entities
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        [Required]
        [Column("OrderItemId")]
        public int OrderItemId { get; set; }

        [Column("OrderId")]
        public int? OrderId { get; set; }
        
        [Column("CategoryId")]
        public int? CategoryId { get; set; }

        [Column("ProductName")]
        [Sieve(CanSort = true)]
        public string ProductName { get; set; }

        [Column("Status")]
        [Sieve(CanSort = true)]
        public string Status { get; set; }

        [Column("Amount")]
        [Sieve(CanSort = true)]
        public int? Amount { get; set; }

        [Column("Comments")]
        public string Comments { get; set; }

        [ForeignKey("OrderId")]
        public CompanyOrder Order { get; set; } = new CompanyOrder { };

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category { };
    }
}
