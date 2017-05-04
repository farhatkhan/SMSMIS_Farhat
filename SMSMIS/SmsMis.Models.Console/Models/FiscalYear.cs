using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Models
{
    namespace SmsMis.Models.Console.Admin
    {
        using System;
        using System.Collections.Generic;
        using System.ComponentModel.DataAnnotations;
        using System.ComponentModel.DataAnnotations.Schema;
        using System.Data.Entity.Spatial;

        [Table("FiscalYear")]
        public partial class FiscalYear
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
            [Column(Order=2)]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int FiscalYearSerial{ get; set; }
            
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DateTime StartDate { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DateTime EndDate { get; set; }
        }
    }
}
