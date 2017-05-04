namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comCompany")]
    public partial class comCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Required]
        [StringLength(255)]
        public string companyName { get; set; }

        public string companyLOGO_URL { get; set; }

        [Required]
        [StringLength(5)]
        public string finYearStart { get; set; }

        [Required]
        [StringLength(5)]
        public string finYearEnd { get; set; }
    }
}
