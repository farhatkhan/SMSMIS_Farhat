namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Party")]
    public partial class Party
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartyCode { get; set; }

        [Required]
        [StringLength(100)]
        public string PartyName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        public bool? CreditParty { get; set; }

        public int? CreditDays { get; set; }

        public double? CreditLimit { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

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
        public string Email { get; set; }

        [StringLength(25)]
        public string STRNo { get; set; }

        [StringLength(25)]
        public string NTN { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string UserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
