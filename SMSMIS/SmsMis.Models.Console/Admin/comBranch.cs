namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comBranch
    {
        [Key]
        public Guid BranchID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int RegionID { get; set; }

        public int CompanyID { get; set; }


        

        [NotMapped]
        public bool BranchIDSelected { get; set; }

        public virtual comCompany comCompanies { get; set; }

        public virtual comRegion comRegions { get; set; }
    }
}
