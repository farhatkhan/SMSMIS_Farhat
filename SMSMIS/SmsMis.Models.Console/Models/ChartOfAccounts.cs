namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChartOfAccounts
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string AccountCode { get; set; }

        [Required]
        [StringLength(255)]
        public string AccountTitle { get; set; }

        [StringLength(255)]
        public string ShortName { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [Required]
        [StringLength(2)]
        public string LevelId { get; set; }

        [StringLength(1)]
        public string AccountType { get; set; }

        [StringLength(255)]
        public string ParentAccountCode { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
        public virtual ICollection<COABranch> COABranch { get; set; }
    }

}
