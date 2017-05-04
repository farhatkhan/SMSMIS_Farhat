namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comUserTypeOperations
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte OperationID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int DepartmentID { get; set; }

        public byte AccessTypeID { get; set; }
        
        [NotMapped]
        public string accessType { get; set; }
        //public virtual IList<comAccessTypes> comAccessType2 { get; set; }
        //public virtual IList<comAccessTypes> comAccessType { get; set; }
    }
}
