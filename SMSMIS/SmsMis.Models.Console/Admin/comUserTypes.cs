namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comUserTypes")]
    public partial class comUserTypes
    {
        public comUserTypes()
        {
            comUserBranch = new List<comUserTypeBranches>();
        }

        [Key]
        public Guid UserTypeID { get; set; }

        [Required(ErrorMessage = "User type is required")]
        [StringLength(20)]
        public string userType { get; set; }
        
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        public Guid BranchID { get; set; }

        public bool? BPMDash { get; set; }

        public bool? DPTDash { get; set; }

        public virtual IList<comUserTypeBranches> comUserBranch { get; set; }

        public virtual IList<comUserTypeDepartment> comUserDepartment { get; set; }

        public virtual comDepartments comDepartment { get; set; }

        public virtual comBranch comBranches { get; set; }
    }
}


