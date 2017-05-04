namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(75)]
        public string CompanyUserLoginId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(75)]
        public string BranchUserLoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
