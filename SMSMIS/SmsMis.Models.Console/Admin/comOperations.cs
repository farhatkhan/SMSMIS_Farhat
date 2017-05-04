using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace SmsMis.Models.Console.Admin
{

    [Table("comOperations")]
    public partial class comOperations
    {
        [Key]
        public byte OperationID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<comDepartmentOperations> comDepartmentOperationList { get; set; }
    }
}
     
