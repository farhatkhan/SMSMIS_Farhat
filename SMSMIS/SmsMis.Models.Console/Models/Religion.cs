namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Religion")]
    public partial class Religion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReligionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ReligionName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }
        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
