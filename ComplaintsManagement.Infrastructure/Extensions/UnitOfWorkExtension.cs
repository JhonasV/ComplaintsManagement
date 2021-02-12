using ComplaintsManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace ComplaintsManagement.Infrastructure.Extensions
{
    public static class UnitOfWorkExtension
    {
        public static UnityContainer SetupUnitOfWork(this UnityContainer container)
        {
            container.RegisterInstance<IUnitOfWork, UnitOfWork>();

            return container;
        }
    }
}