namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comUsers
    {
        [Key]
        public Guid UserID { get; set; }

        [StringLength(15)]
        public string FName { get; set; }

        [StringLength(15)]
        public string LName { get; set; }

        public Guid UserTypeID { get; set; }

        [StringLength(15)]
        public string userLogin { get; set; }

        [StringLength(10)]
        public string userPWD { get; set; }

        public int DepartmentID { get; set; }

        public Guid BranchID { get; set; }

        public int createdBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime createdTime { get; set; }

        public bool isClosed { get; set; }

        public DateTime? lastTickTime { get; set; }
    }
}
