namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeBillDetailMonthly")]
    public partial class FeeBillDetailMonthly
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
        public int SessionCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime TransactionDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChallanNo { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SrNo { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParticularCode { get; set; }

        public int StudentNo { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal NetAmount { get; set; }

        public DateTime? VoucherDate { get; set; }

        public int? VoucherCode { get; set; }

        public int? VoucherNo { get; set; }
    }
}
