namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cltUsers
    {
        public int? UserID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string userLogin { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string userPassword { get; set; }

        public int? CompanyCode { get; set; }

        public int? BranchCode { get; set; }
    }
}
