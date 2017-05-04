namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COALevels
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column("COALevels", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COALevels1 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string LevelId { get; set; }

        public int LevelLength { get; set; }

        public double LevelColor { get; set; }
    }
}
