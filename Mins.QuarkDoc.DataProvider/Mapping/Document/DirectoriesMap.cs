using Mins.QuarkDoc.DataEntities;
namespace Mins.QuarkDoc.DataProvider
{
    public class DirectoriesMap : BaseMapping<Directories>
    {
        public DirectoriesMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Directories");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ApplicationId).HasColumnName("ApplicationId");
            this.Property(t => t.DirectoryName).HasColumnName("DirectoryName");
            this.Property(t => t.DirectoryId).HasColumnName("DirectoryId");
            this.Property(t => t.Sort).HasColumnName("Sort");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
        }
    }
}
