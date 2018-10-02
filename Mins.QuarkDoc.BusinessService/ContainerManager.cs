using Microsoft.Practices.Unity;
using Mins.QuarkDoc.DataInterface;
using Mins.QuarkDoc.DataProvider;
namespace Mins.QuarkDoc.BusinessService
{
    public static class ContainerManager<TEntiy> where TEntiy : class
    {
        public static IUnityContainer GetContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IRepository<TEntiy>, Repository<TEntiy>>();
            return container;
        }

        /// <summary>
        /// 根据容器解析公共接口
        /// </summary>
        /// <returns></returns>
        public static IRepository<TEntiy> GetResolve()
        {
            return GetContainer().Resolve<IRepository<TEntiy>>();
        }
    }
}
