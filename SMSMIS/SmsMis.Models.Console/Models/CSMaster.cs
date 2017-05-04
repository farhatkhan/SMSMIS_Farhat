namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSMaster")]
    public partial class CSMaster
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportName { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportTitle { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [Required]
        [StringLength(25)]
        public string UserId { get; set; }

        public DateTime AddDateTime { get; set; }

        public virtual ICollection<CSDetail> CSDetail { get; set; }
    }
}
