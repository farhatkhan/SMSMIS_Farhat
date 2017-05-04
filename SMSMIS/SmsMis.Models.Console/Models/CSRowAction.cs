namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSRowAction")]
    public partial class CSRowAction
    {
        [Key]
        [StringLength(25)]
        public string Description { get; set; }
    }
}
