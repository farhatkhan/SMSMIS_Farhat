namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KinshipDiscount")]
    public partial class KinshipDiscount
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
        [StringLength(10)]
        public string KinshipType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string KinshipRelation { get; set; }

        public int Discount { get; set; }
    }
}
