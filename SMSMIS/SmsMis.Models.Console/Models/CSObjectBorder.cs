namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSObjectBorder")]
    public partial class CSObjectBorder
    {
        [Key]
        [StringLength(25)]
        public string ObjectBorder { get; set; }
    }
}
