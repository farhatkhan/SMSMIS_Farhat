namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstrumentSerialDetail")]
    public partial class InstrumentSerialDetail
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
        [StringLength(255)]
        public string AccountCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstrumentTypeCode { get; set; }

        [Key]
        [Column(Order = 4)]
        public double InstrumentNo { get; set; }

        public bool Cancelled { get; set; }

        [StringLength(255)]
        public string CancelledReason { get; set; }

        [StringLength(25)]
        public string CancelledByUserId { get; set; }

        public DateTime? CancelledDateTime { get; set; }
    }
}
