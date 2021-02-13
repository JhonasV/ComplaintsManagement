using ComplaintsManagement.Infrastructure.Configurations;
using ComplaintsManagement.UI.Controllers;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using ComplaintsManagement.UI.Services.Repositories;
using FluentValidation;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace ComplaintsManagement.UI.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ICustomersRepository, CustomersRepository>();
            container.RegisterType<IProductsRepository, ProductsRepository>();
            container.RegisterType<IValidatorFactory, FluentValidationConfiguration>(new ContainerControlledLifetimeManager());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}