using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NottCS.Services.Data;
using NottCS.Services.Data.Club;
using NottCS.Services.Data.Member;
using NottCS.Services.Data.User;
using NottCS.Services.Data.User.NottCS.Services.Data.Club;
using NottCS.Services.LoginService;
using NottCS.Services.Navigation;
using NottCS.Services.Stuff;
using NottCS.Services.Sync;
using NottCS.ViewModels;
using NottCS.ViewModels.Club;
using NottCS.ViewModels.Event;
using NottCS.ViewModels.Test;

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

            builder.RegisterType<BackendService.BackendService>().AsSelf().SingleInstance();
            builder.RegisterType<LocalDatabaseConnection>().AsSelf().InstancePerDependency();
            builder.RegisterType<SyncService>().AsSelf().SingleInstance();

            //data services
            builder.RegisterType<ClubService>().As<IClubService>().SingleInstance();
            builder.RegisterType<MemberService>().As<IMemberService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
        }

        void RegisterViewModels(ref ContainerBuilder builder)
        {
            builder.RegisterType<DITestViewModel>().InstancePerDependency();
            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<MenuViewModel>().InstancePerDependency();
            builder.RegisterType<LoginViewModel>().InstancePerDependency();
            builder.RegisterType<HomeViewModel>().InstancePerDependency();
            builder.RegisterType<ClubListViewModel>().InstancePerDependency();
            builder.RegisterType<EventListViewModel>().InstancePerDependency();
            builder.RegisterType<ProfileViewModel>().InstancePerDependency();
            builder.RegisterType<ClubViewModel>().InstancePerDependency();
            builder.RegisterType<AdminPanelViewModel>().InstancePerDependency();
            //Test
            builder.RegisterType<DatabaseTestViewModel>().InstancePerDependency();
        }

        public DITestViewModel DITest => _container.Resolve<DITestViewModel>();
        public MainViewModel Main => _container.Resolve<MainViewModel>();
        public MenuViewModel Menu => _container.Resolve<MenuViewModel>();
        public LoginViewModel Login => _container.Resolve<LoginViewModel>();
        public HomeViewModel Home => _container.Resolve<HomeViewModel>();
        public ClubListViewModel ClubList => _container.Resolve<ClubListViewModel>();
        public EventListViewModel EventList => _container.Resolve<EventListViewModel>();
        public ProfileViewModel Profile => _container.Resolve<ProfileViewModel>();
        public ClubViewModel Club => _container.Resolve<ClubViewModel>();
        public AdminPanelViewModel AdminPanel => _container.Resolve<AdminPanelViewModel>();
        public DatabaseTestViewModel DatabaseTest => _container.Resolve<DatabaseTestViewModel>();
    }
}
