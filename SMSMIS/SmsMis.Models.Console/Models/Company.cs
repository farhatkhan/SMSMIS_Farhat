namespace SmsMis.Models.Console.Handlers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Required]
        [StringLength(75)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(50)]
        public string Salogan { get; set; }

        [StringLength(75)]
        public string ContactPerson { get; set; }

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
    }
}
