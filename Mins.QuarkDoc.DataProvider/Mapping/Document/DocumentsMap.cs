using Mins.QuarkDoc.DataEntities;
namespace Mins.QuarkDoc.DataProvider
{
    public class DocumentsMap : BaseMapping<Documents>
    {
        public DocumentsMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Documents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.DirectoryId).HasColumnName("DirectoryId");
            this.Property(t => t.Document).HasColumnName("Document");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.Sort).HasColumnName("Sort");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ApplicationId).HasColumnName("ApplicationId");
        }
    }
}
