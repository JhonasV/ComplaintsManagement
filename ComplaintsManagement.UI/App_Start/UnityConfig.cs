﻿using ComplaintsManagement.UI.Controllers;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using ComplaintsManagement.UI.Services.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
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
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<ICustomersRepository, CustomersRepository>();
            container.RegisterType<IProductsRepository, ProductsRepository>();
            container.RegisterType<ICustomersProductsRepository, CustomerProductsRepository>();
            container.RegisterType<IComplaintsOptionsRepository, ComplaintsOptionsRepository>();
            container.RegisterType<IComplaintsRepository, ComplaintsRepository>();
            container.RegisterType<IStatusRepository, StatusRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}