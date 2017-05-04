namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherDetail")]
    public partial class VoucherDetail
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime VoucherDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VoucherCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VoucherNo { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SrNo { get; set; }

        [StringLength(255)]
        public string AccountCode { get; set; }

        [StringLength(255)]
        public string Narration { get; set; }

        public int? CCCode { get; set; }

        public int? EmployeeCode { get; set; }

        public int? ProjectCode { get; set; }

        public int? AnalysisCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? FC_Debit { get; set; }

        [Column(TypeName = "money")]
        public decimal? FC_Credit { get; set; }

        [Column(TypeName = "money")]
        public decimal? LC_Debit { get; set; }

        [Column(TypeName = "money")]
        public decimal? LC_Credit { get; set; }

       // public VoucherMaster VoucherMaster { get; set; }
    }
}
