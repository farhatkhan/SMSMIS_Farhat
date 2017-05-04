namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeParticularRecurringType")]
    public partial class FeeParticularRecurringType
    {
        [Key]
        [StringLength(50)]
        public string Recurring { get; set; }
    }
}
