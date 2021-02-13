using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace ComplaintsManagement.Infrastructure.Configurations
{
    public class FluentValidationConfiguration : ValidatorFactoryBase
    {
            private readonly IUnityContainer _container;

            public FluentValidationConfiguration(IUnityContainer container)
            {
                _container = container;
            }

            public override IValidator CreateInstance(Type validatorType)
            {
                return _container.Resolve(validatorType) as IValidator;
            }
    }   
}