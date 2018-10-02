using Mins.QuarkDoc.DataEntities;
namespace Mins.QuarkDoc.DataProvider
{
    public class UserMap : BaseMapping<User>
    {
        public UserMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsAdmin).HasColumnName("UserName");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
