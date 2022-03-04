using Autofac;
using MealOrdering.Business.Abstract;
using MealOrdering.Business.Concrete;
using MealOrdering.Core.Utilities.Security.Jwt;
using MealOrdering.Repository.Abstract;
using MealOrdering.Repository.Concrete;
using MealOrdering.Repository.Concrete.EntityFramework;

namespace MealOrdering.Business.DependencyResolvers.AutoFac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Core

            builder.RegisterType<JwtHelper>().As<IJwtHelper>();

            #endregion

            #region Business

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<SubOrderManager>().As<ISubOrderService>();
            builder.RegisterType<SupplierManager>().As<ISupplierService>();

            #endregion

            #region Repository

            builder.RegisterGenericComposite(typeof(EfGenericRepositoryBase<>), typeof(IGenericRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<EfOrderRepository>().As<IOrderRepository>();
            builder.RegisterType<EfSubOrderRepository>().As<ISubOrderRepository>();
            builder.RegisterType<EfSupplierRepository>().As<ISupplierRepository>();
            builder.RegisterType<EfUserRepository>().As<IUserRepository>();

            #endregion
        }
    }
}
