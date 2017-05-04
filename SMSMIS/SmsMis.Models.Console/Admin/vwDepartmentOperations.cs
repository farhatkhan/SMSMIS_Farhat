namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vwDepartmentOperations
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int departmentid { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte OperationID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int isSelected { get; set; }
    }
}
