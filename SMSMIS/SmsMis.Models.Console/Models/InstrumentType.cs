namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstrumentType")]
    public partial class InstrumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstrumentCode { get; set; }

        [Required]
        [StringLength(50)]
        public string InstrumentName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        public bool ManageSerial { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
