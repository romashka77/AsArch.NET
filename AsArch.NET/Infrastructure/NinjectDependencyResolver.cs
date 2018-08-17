using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsArch.NET.EntityDataModel;
using AsArch.NET.Interfaces;
using Ninject;
using Ninject.Web.Common;

namespace AsArch.NET.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {//InRequestScope//InThreadScope
            kernel.Bind<DB_pfr_sap>().ToSelf();//.InRequestScope();
            kernel.Bind<IRepository>().To<Repository>();//.InRequestScope();//WithConstructorArgument("context", new DB_pfr_sap());
        }
    }
}