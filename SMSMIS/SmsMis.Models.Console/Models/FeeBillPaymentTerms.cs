namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FeeBillPaymentTerms
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string BillType { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SrNo { get; set; }

        [Required]
        [StringLength(255)]
        public string PaymentTerm { get; set; }

        [Required]
        [StringLength(50)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
