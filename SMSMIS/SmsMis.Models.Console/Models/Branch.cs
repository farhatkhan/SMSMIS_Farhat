namespace SmsMis.Models.Console.Handlers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using SmsMis.Models.Console.Admin;

    [Table("Branch")]
    public class Branch
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchCode { get; set; }

        [Required]
        [StringLength(75)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(50)]
        public string Salogan { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string Phone1 { get; set; }

        [StringLength(25)]
        public string Phone2 { get; set; }

        [StringLength(25)]
        public string Phone3 { get; set; }

        [StringLength(25)]
        public string Phone4 { get; set; }

        [StringLength(25)]
        public string Fax1 { get; set; }

        [StringLength(25)]
        public string Fax2 { get; set; }

        [StringLength(50)]
        public string URL { get; set; }

        [Required]
        [StringLength(50)]
        public string Eamil1 { get; set; }

        [StringLength(50)]
        public string Email2 { get; set; }

        [StringLength(25)]
        public string STRNo { get; set; }

        [StringLength(25)]
        public string NTN { get; set; }

        [StringLength(1000)]
        public string LogoPath { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }

        public virtual ICollection<BranchContactPerson> BranchContactPersonList { get; set; }
    }
}
