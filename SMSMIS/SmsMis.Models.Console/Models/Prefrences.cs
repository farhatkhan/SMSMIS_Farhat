namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prefrences
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchCode { get; set; }

        public int CashVoucherCode { get; set; }

        [Required]
        [StringLength(255)]
        public string IncomeAccountCode { get; set; }

        public int LocalCurrencyCode { get; set; }

        public int StudentBillingCycle { get; set; }
    }
}
