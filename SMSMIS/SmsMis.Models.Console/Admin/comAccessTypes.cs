namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comAccessTypes
    {
        [Key]
        public byte AccessTypeID { get; set; }

        [Required]
        [StringLength(25)]
        public string accessType { get; set; }
    }
}
