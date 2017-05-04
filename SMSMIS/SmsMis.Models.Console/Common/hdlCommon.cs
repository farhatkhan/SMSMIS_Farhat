using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using System.Data.Entity.Infrastructure;
using System.Web;
namespace SmsMis.Models.Console.Common
{
    public class hdlCommon
    {
        public IList<LinksInfo> getLinks(string adminID)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            DbRawSqlQuery<LinksInfo> data = db.Database.SqlQuery<LinksInfo>
                //("Select isnull(parent,0) as parentId,LinkID  as linkID,label as linkLabel,url as url from vwAdminAccess where accesstypeid=1 and Adminid=@p0 order by linkid,sortorder", adminID);
                ("Select isnull(parent,0) as parentId,LinkID  as linkID,label as linkLabel,url as url from vwAdminAccess order by linkid,sortorder", adminID);

            return data.ToArray();
            
            
        }

        public IList<LinksInfo> getClientLinks(string userID)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            DbRawSqlQuery<LinksInfo> data = db.Database.SqlQuery<LinksInfo>
                ("Select isnull(parent,0) as parentId,LinkID  as linkID,label as linkLabel,url as url from vwClientAccess where accesstypeid=1 and UserID=@p0 order by linkid,sortorder", userID);

            return data.ToArray();


        }
        //public IList<LinksInfo> getLinks(Guid UserID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    DbRawSqlQuery<LinksInfo> data = db.Database.SqlQuery<LinksInfo>
        //        ("Select isnull(parent,0) as parentId,LinkID  as linkID,label as linkLabel,url as url from vwUserAccess where accesstypeid=1 and UserID=@p0 order by linkid,sortorder", UserID);
        //    return data.ToArray();
        //}
    }
}
