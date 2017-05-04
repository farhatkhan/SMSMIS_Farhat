namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
 
        [TrackChanges]
    public partial class comDepartmentOperations
    {
        [Column(Order = 0)]
            [Key]
        public byte OperationID { get; set; }

        [Column(Order = 1)]
        [Key]
        public int DepartmentID { get; set; }

        //public virtual comDepartments comDepartments { get; set; }

        //public virtual comOperations comOperations { get; set; }

    
    }
}
