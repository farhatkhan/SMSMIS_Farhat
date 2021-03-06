namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Valency.Models.Console.Client;

    [Table("AdmissionInquiry")]
    public partial class AdmissionInquiry
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
        public int FormNo { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(6)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(5)]
        public string BloodGroup { get; set; }

        public int ClassCode { get; set; }

        [StringLength(75)]
        public string LastInstitute { get; set; }

        [StringLength(50)]
        public string StudentNICNo { get; set; }

        [StringLength(25)]
        public string StudentCellNo { get; set; }

        [StringLength(50)]
        public string StudentEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(25)]
        public string Landline1 { get; set; }

        [StringLength(25)]
        public string Landline2 { get; set; }

        [StringLength(75)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string FatherOccupation { get; set; }

        [StringLength(50)]
        public string FatherNICNo { get; set; }

        [StringLength(25)]
        public string FatherCellNo { get; set; }

        [StringLength(50)]
        public string FatherEmail { get; set; }

        [StringLength(255)]
        public string FatherOfficeAddress { get; set; }

        [StringLength(255)]
        public string FatherOfficePhone { get; set; }

        [StringLength(75)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string MotherOccupation { get; set; }

        [StringLength(50)]
        public string MotherNICNo { get; set; }

        [StringLength(25)]
        public string MotherCellNo { get; set; }

        [StringLength(50)]
        public string MotherEmail { get; set; }

        [StringLength(255)]
        public string MotherOfficeAddress { get; set; }

        [StringLength(25)]
        public string MotherOfficePhone { get; set; }

        [StringLength(75)]
        public string GuardianName { get; set; }

        [StringLength(50)]
        public string GuardianOccupation { get; set; }

        [StringLength(50)]
        public string GuardianNICNo { get; set; }

        [StringLength(25)]
        public string GuardianCellNo { get; set; }

        [StringLength(50)]
        public string GuardianEmail { get; set; }

        [StringLength(255)]
        public string GuardianOfficeAddress { get; set; }

        [StringLength(25)]
        public string GuardianOfficePhone { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public DateTime? VoucherDate { get; set; }

        public int? VoucherCode { get; set; }

        public int? VoucherNo { get; set; }

        [Required]
        [StringLength(50)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }

        public virtual ICollection<AdmissionEnquiryMarketingReference> AdmissionEnquiryMarketingReference { get; set; }

    }
}
