namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BranchContactPerson")]
    public partial class BranchContactPerson
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
        public int SrNo { get; set; }

        [Required]
        [StringLength(75)]
        public string ContactPerson { get; set; }

        [Required]
        [StringLength(25)]
        public string LandLine { get; set; }

        [Required]
        [StringLength(25)]
        public string Cell { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
