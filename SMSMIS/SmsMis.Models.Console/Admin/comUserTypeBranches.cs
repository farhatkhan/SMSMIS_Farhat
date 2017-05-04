namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("comUserTypeBranches")]
    public partial class comUserTypeBranches
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid BranchID { get; set; }

        //[NotMapped]
        //public bool isSelected { get; set; }

        //public virtual comUserTypes comUserTypes { get; set; }
    
    }
}
