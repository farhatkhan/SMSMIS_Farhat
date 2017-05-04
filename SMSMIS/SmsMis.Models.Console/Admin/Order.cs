namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Customer { get; set; }

        public DateTime? OrderDate { get; set; }

        [StringLength(5)]
        public string Status { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }

    }
}
