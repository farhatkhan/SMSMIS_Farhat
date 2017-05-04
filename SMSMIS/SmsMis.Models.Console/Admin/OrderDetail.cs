namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        [StringLength(64)]
        public string Product { get; set; }

        public int? Amount { get; set; }

        public decimal? UnitPrice { get; set; }

        //public int? Order_Id { get; set; }
    }
}
