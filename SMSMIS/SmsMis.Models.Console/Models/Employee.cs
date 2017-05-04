namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(9)]
        public string MaritalStatus { get; set; }

        [StringLength(100)]
        public string HusbandName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string PlaceOfBirth { get; set; }

        [Required]
        [StringLength(25)]
        public string CNIC { get; set; }

        public DateTime CNICValidityDate { get; set; }

        [StringLength(25)]
        public string PassportNo { get; set; }

        public DateTime? PassportValidityDate { get; set; }

        [StringLength(50)]
        public string IdentificationMark { get; set; }

        public int? NationalityCode { get; set; }

        public int? ReligionCode { get; set; }

        [StringLength(25)]
        public string CellNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(5)]
        public string BloodGroup { get; set; }

        [StringLength(255)]
        public string DisabilityDetail { get; set; }

        [StringLength(255)]
        public string PresentAddress { get; set; }

        [StringLength(100)]
        public string PresentAddressPoliceStation { get; set; }

        [StringLength(25)]
        public string PresentAddressPhoneNo { get; set; }

        [StringLength(255)]
        public string PermanentAddress { get; set; }

        [StringLength(100)]
        public string PermanentAddressPoliceStation { get; set; }

        [StringLength(25)]
        public string PermanentAddressPhoneNo { get; set; }

        [StringLength(255)]
        public string PostalAddress { get; set; }

        [StringLength(100)]
        public string PostalAddressPoliceStation { get; set; }

        [StringLength(25)]
        public string PostalAddressPhoneNo { get; set; }

        [StringLength(255)]
        public string EmergencyContactNos { get; set; }

        [StringLength(255)]
        public string EmployeePhotoPath { get; set; }

        [StringLength(255)]
        public string EmployeeSignaturePath { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(50)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
