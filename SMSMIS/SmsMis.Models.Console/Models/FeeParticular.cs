namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeParticular")]
    public partial class FeeParticular
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParticularCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ParticularName { get; set; }

        [Required]
        [StringLength(50)]
        public string Recurring { get; set; }

        public bool FirstFeeParticular { get; set; }

        public bool RegularFeeParticular { get; set; }

        public bool ScholarshipAllowed { get; set; }

        public bool DiscountAllowed { get; set; }

        public bool Optional { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(50)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
