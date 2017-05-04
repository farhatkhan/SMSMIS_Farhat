namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeReceipt")]
    public partial class FeeReceipt
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
        public int ReceiptNo { get; set; }

        public DateTime ReceiptDate { get; set; }

        public int SessionCode { get; set; }

        public int ChallanNo { get; set; }

        [Required]
        [StringLength(6)]
        public string ReceivedAt { get; set; }

        public int InstrumentCode { get; set; }

        [StringLength(25)]
        public string InstrumentNo { get; set; }

        public DateTime? InstrumentDate { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [Column(TypeName = "money")]
        public decimal OutstandingAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal WaivedAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal ReceivedAmount { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
