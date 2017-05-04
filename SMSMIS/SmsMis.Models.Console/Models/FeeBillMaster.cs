namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeBillMaster")]
    public partial class FeeBillMaster
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SessionCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChallanNo { get; set; }

        [Required]
        [StringLength(1)]
        public string ChallanType { get; set; }

        public int StudentNo { get; set; }

        public int ClassCode { get; set; }

        [Required]
        [StringLength(255)]
        public string FeeTerm { get; set; }

        [Required]
        [StringLength(255)]
        public string FeePeriod { get; set; }

        public int BankCode { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal ScholarshipAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal AttendanceFineAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal NetAmount { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }

        public virtual ICollection<FeeBillDetail> FeeBillDetail { get; set; }
        public virtual ICollection<FeeBillDetailMonthly> FeeBillDetailMonthly { get; set; }

    }
}
