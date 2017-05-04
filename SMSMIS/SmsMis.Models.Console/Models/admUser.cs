namespace SmsMis.Models.Console.Handlers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class admUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Login")]
        public string adminLogin { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string adminPassword { get; set; }
    }
}
