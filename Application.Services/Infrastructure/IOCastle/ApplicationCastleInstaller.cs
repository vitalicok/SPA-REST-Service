using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Application.Data.Interfaces;
using Application.Data.Infrastructure;
using System.Reflection;
using System.Web.Mvc;

namespace Application.Services.Infrastructure.IOCastle
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryData>().ImplementedBy<RepositoryData>());


            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();

            controllers.ForEach(c => {
                container.Register(Component.For(c).LifestylePerWebRequest());
            });
        }
    }
}