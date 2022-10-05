using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pesmissionbase.Models;

namespace pesmissionbase.Data
{
    public class DataBaseContext : IdentityDbContext<Users, Roles, string>
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {


        }
        public DbSet<UsersPermisions> UsersPermisions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<CandA> CandA { get; set; }
        public DbSet<Grouping> Grouping { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Permissions>().HasData(new Permissions {PermissionsId="1" ,ControlerName = "karname", ActionName = "show" ,UsersPermisions=new List<UsersPermisions>()});
        //    modelBuilder.Entity<Permissions>().HasData(new Permissions { PermissionsId = "2" , ControlerName = "karname", ActionName = "edit" , UsersPermisions = new List<UsersPermisions>() });


        //}
    }
}
