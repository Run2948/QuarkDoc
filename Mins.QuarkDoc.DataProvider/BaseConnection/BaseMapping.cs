using System.Data.Entity.ModelConfiguration;

namespace Mins.QuarkDoc.DataProvider
{
    public class BaseMapping<TEntiy> : EntityTypeConfiguration<TEntiy> where TEntiy : class
    {

    }
}
