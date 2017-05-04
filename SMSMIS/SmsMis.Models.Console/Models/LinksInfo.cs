using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class LinksInfo
    {
        //parentId, linkId, linkText, url|parentId, parentText, url
        public int parentId { get; set; }
        public int linkID { get; set; }
        public string linkLabel { get; set; }
        public string url { get; set; }
    }
}
