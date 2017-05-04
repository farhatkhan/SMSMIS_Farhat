namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bank")]
    public partial class Bank
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string AccountCode { get; set; }

        [Required]
        [StringLength(50)]
        public string BankName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(50)]
        public string BankBranch { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(25)]
        public string Phone1 { get; set; }

        [StringLength(25)]
        public string Phone2 { get; set; }

        [StringLength(25)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Addressee { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountTitle { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }

        [Required]
        [StringLength(25)]
        public string AccountNo { get; set; }
    }
}
