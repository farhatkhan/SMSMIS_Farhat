namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSFontStyle")]
    public partial class CSFontStyle
    {
        [Key]
        [StringLength(25)]
        public string FontStyle { get; set; }
    }
}
