//------------------------------------------------------------------------------
//  Name: Data Access Layer
//  Last modified by: Raquel Chavez
//  Date: 06/09/2017
//  Notes: Context class to manage entity classes and to access data
//         data in the database.
// 
//------------------------------------------------------------------------------

namespace ReleaseMgmtWeb.DAL
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class ReleaseMgmtDataEntities : DbContext
    {
        public ReleaseMgmtDataEntities()
            : base("name=ReleaseMgmtDataEntities")
        {
            /*Set initializer, one uncomment below*/

            //Database.SetInitializer<ReleaseMgmtDataEntities>(new CreateDatabaseIfNotExists<ReleaseMgmtDataEntities>());
            //Database.SetInitializer<ReleaseMgmtDataEntities>(new DropCreateDatabaseIfModelChanges<ReleaseMgmtDataEntities>());
            //Database.SetInitializer<ReleaseMgmtDataEntities>(new DropCreateDatabaseAlways<ReleaseMgmtDataEntities>());
            //Database.SetInitializer<ReleaseMgmtDataEntities>(new ReleaseMgmtInitializer());
            Database.SetInitializer<ReleaseMgmtDataEntities>(null);
            //Database.SetInitializer(new ReleaseMgmtInitializer());
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}