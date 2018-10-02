using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
namespace Mins.QuarkDoc.BusinessService
{
    public class UserBusinessService : DbServiceBase<User>
    {
        public User Login(string eamil, string password)
        {
            User user = FirstOrDefault(new DirectSpecification<User>(t => t.Email == eamil && t.Password == password & t.IsEnabled));
            return user;
        }
    }
}
