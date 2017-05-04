namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("loginAcitivtylog")]
    public partial class loginAcitivtylog
    {
        [Key]
        public Int64 LoginActivityId { get; set; }

        public byte LoginTypeId { get; set; }

        public int adminID { get; set; }

        [StringLength(20)]
        public string IP { get; set; }

        public DateTime ActivityDateTime { get; set; }

        public Guid UserID { get; set; }
    }
}
