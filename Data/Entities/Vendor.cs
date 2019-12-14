using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Entities
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        [Required]
        [Column("VendorId")]
        public int VendorId { get; set; }

        [Column("VendorName")]
        public string VendorName { get; set; }

    }
}
