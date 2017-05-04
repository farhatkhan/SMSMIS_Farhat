using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SmsMis.Models.Console.Admin
{

[TrackChanges]
[Table("comDepartments")]
    public partial class comDepartments
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } 

        [StringLength(500)]
        public string Description { get; set; }
    
        public virtual ICollection<comDepartmentOperations> comUserOperationList { get; set; }
    }
}
