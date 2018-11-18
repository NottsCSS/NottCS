using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NottCS.Services.LoginService;
using NottCS.Services.Navigation;
using NottCS.Services.Stuff;
using NottCS.ViewModels;
using NottCS.ViewModels.Club;
using NottCS.ViewModels.Event;

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
            
            //registering services
            builder.RegisterType<StuffService>().As<IStuffService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<LoginService.LoginService>().As<ILoginService>().SingleInstance();
        }

        void RegisterViewModels(ref ContainerBuilder builder)
        {
            builder.RegisterType<AboutViewModel>().InstancePerDependency();
            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<MenuViewModel>().InstancePerDependency();
            builder.RegisterType<LoginViewModel>().InstancePerDependency();
            builder.RegisterType<HomeViewModel>().InstancePerDependency();
            builder.RegisterType<ClubListViewModel>().InstancePerDependency();
            builder.RegisterType<EventListViewModel>().InstancePerDependency();
            builder.RegisterType<ProfileViewModel>().InstancePerDependency();
            builder.RegisterType<ClubViewModel>().InstancePerDependency();
            builder.RegisterType<AdminPanelViewModel>().InstancePerDependency();
        }

        public AboutViewModel About => _container.Resolve<AboutViewModel>();
        public MainViewModel Main => _container.Resolve<MainViewModel>();
        public MenuViewModel Menu => _container.Resolve<MenuViewModel>();
        public LoginViewModel Login => _container.Resolve<LoginViewModel>();
        public HomeViewModel Home => _container.Resolve<HomeViewModel>();
        public ClubListViewModel ClubList => _container.Resolve<ClubListViewModel>();
        public EventListViewModel EventList => _container.Resolve<EventListViewModel>();
        public ProfileViewModel Profile => _container.Resolve<ProfileViewModel>();
        public ClubViewModel Club => _container.Resolve<ClubViewModel>();
        public AdminPanelViewModel AdminPanel => _container.Resolve<AdminPanelViewModel>();
    }
}
