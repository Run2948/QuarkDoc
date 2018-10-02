using Mins.QuarkDoc.DataEntities;
namespace Mins.QuarkDoc.DataProvider
{
    public class ApplicationMap : BaseMapping<Application>
    {
        public ApplicationMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Application");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.HasOptional(t => t.User).WithMany().HasForeignKey(d => d.UserId);
        }
    }
}
