using System.Data.Entity;
namespace Mins.QuarkDoc.DataProvider
{
    public class DBContext<TEntiy> : DbContext where TEntiy : class
    {
        static DBContext()
        {
            Database.SetInitializer<DBContext<TEntiy>>(null);
        }
        public DBContext()
           : base("Name=DBContext")
        {
        }
        public DbSet<TEntiy> Entity { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
