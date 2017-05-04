namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentAdmission")]
    public partial class StudentAdmission
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string StudentRollNo { get; set; }

        public int BranchCode { get; set; }

        public int BuildingCode { get; set; }

        public int SessionCode { get; set; }

        public int ClassCode { get; set; }

        public int CourseCode { get; set; }

        public int SectionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
