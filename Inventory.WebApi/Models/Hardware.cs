namespace Inventory.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hardware")]
    public partial class Hardware
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        public string ImageUrl { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}
