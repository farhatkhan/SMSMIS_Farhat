namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CSDetail")]
    public partial class CSDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int ReportCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SrNo { get; set; }

        public int GroupId { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string SubDescription { get; set; }

        public bool Main { get; set; }

        [Required]
        public int NoteCode { get; set; }

        [StringLength(25)]
        public string NoteDataAs { get; set; }

        public bool InverseSign { get; set; }

        [StringLength(25)]
        public string RowAction { get; set; }

        [StringLength(25)]
        public string RowFormula { get; set; }

        public double? FontSize { get; set; }

        [Required]
        [StringLength(25)]
        public string FontStyle { get; set; }

        public bool FontUnderline { get; set; }

        [Required]
        [StringLength(25)]
        public string TopBorder { get; set; }

        [Required]
        [StringLength(25)]
        public string BottomBorder { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public int? LeftGroup { get; set; }

        public int? RightGroup { get; set; }

        [StringLength(1)]
        public string Operator { get; set; }
    }
}
