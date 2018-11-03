using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NottCS.Services.Navigation;
using NottCS.Services.Stuff;
using NottCS.ViewModels;

namespace NottCS.Services
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            RegisterServices(ref builder);
            RegisterViewModels(ref builder);

            _container = builder.Build();
        }

        void RegisterServices(ref ContainerBuilder builder)
        {
            //registering logger
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new NLogLoggerProvider());
            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));
            
            builder.RegisterType<StuffService>().As<IStuffService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
        }

        void RegisterViewModels(ref ContainerBuilder builder)
        {
            builder.RegisterType<AboutViewModel>().InstancePerDependency();
            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<MenuViewModel>().InstancePerDependency();
        }

        public AboutViewModel About => _container.Resolve<AboutViewModel>();
        public MainViewModel Main => _container.Resolve<MainViewModel>();
        public MenuViewModel Menu => _container.Resolve<MenuViewModel>();
    }
}
