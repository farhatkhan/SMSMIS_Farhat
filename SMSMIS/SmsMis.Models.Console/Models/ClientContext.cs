namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SmsMis.Models.Console.Handlers.Admin;
    //public partial class ClientContext : DbContext
    //{
    //    public ClientContext()
    //        : base("name=SmsMisEntities")
    //    {
    //    }

        
    //    //public cltUsers performLogin(cltUsers user)
    //    //{
    //    //    string encPassword = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(user.userPassword));
    //    //    var dbLogin = from lg in cltUsers
    //    //                       where lg.userLogin == user.userLogin && lg.userPassword == encPassword
    //    //                       select lg;
    //    //    if (dbLogin != null)
    //    //    {
    //    //        return dbLogin.First();
    //    //    }
    //    //    else return null;
    //    //}
    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //    }
    //}
}
