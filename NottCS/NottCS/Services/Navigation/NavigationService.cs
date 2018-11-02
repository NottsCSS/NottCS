using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using NottCS.ViewModels;
using Xamarin.Forms;

namespace NottCS.Services.Navigation
{
    internal class NavigationService : INavigationService
    {
        private readonly ILog _logger;
        public NavigationService(ILog logger)
        {
            _logger = logger;
        }

        private bool IsNavigating { get; set; }

        /// <summary>
        /// Used to determine the correct first page on app startup
        /// Handles all authentication on app startup
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Navigates using generic, preferred way of navigation due to type checks during compile time
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="navigationParameter">parameter to be passed during navigation</param>
        /// <returns></returns>
        public async Task NavigateToAsync<TViewModel>(object navigationParameter = null) where TViewModel : BaseViewModel, new()
        {
            await NavigateToAsync(typeof(TViewModel), navigationParameter);
        }

        public async Task NavigateToAsync(Type viewModelType, object navigationParameter = null)
        {
            if (!IsNavigating) //prevents simultaneous navigation
            {
                IsNavigating = true;
                Page page = null;
                var createPageTask = CreatePage(viewModelType);

                if (viewModelType == null || !viewModelType.IsSubclassOf(typeof(BaseViewModel)))
                {
                    _logger.Error($"Attempted navigation to {viewModelType} failed because it does not inherit BaseViewModel");
                    _logger.Info("Navigation terminated prematurely");
                    IsNavigating = false;
                    return;
                }


                //Creating page
                try
                {
                    page = await createPageTask;
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }

                if (page == null)
                {
                    _logger.Error("Page unable to be created");
                    _logger.Info("Navigation terminated prematurely");
                    IsNavigating = false;
                    return;
                }
//                if (viewModelType == typeof(LoginViewModel) || viewModelType == typeof(HomeViewModel))
//                {
//                    ClearNavigation();
//                }
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    var previousPage = navigationPage.CurrentPage;
                    Task pushPageTask = navigationPage.Navigation.PushAsync(page);
                    Task initializeAsyncTask = null;
                    if (page.BindingContext is BaseViewModel viewModel)
                    {
                        initializeAsyncTask = viewModel.InitializeAsync(navigationParameter);
                    }
                    else
                    {
                        _logger.Error($"Navigation failed, {page.GetType()} has BindingContext that does not inherit BaseViewModel, " +
                                      $"BindingContext has type: {page.BindingContext.GetType()}");
                        IsNavigating = false;
                        return;
                    }

                    _logger.Debug($"Previous page is: {previousPage}");
                    _logger.Info($"Navigation to {page} started");
                    await pushPageTask;
                    if (initializeAsyncTask != null) await initializeAsyncTask;
                    _logger.Info($"Navigation to {page} ended");
                }
                else
                {
                    _logger.Info($"MainPage is not Navigation page, will be replaced with new page");
                    Task initializeAsyncTask = null;
                    if (page.BindingContext is BaseViewModel viewModel)
                    {
                        initializeAsyncTask = viewModel.InitializeAsync(navigationParameter);
                    }
                    else
                    {
                        _logger.Error($"Navigation failed, {page.GetType()} has BindingContext that does not inherit BaseViewModel, " +
                                      $"BindingContext has type: {page.BindingContext.GetType()}");
                        IsNavigating = false;
                        return;
                    }

                    Application.Current.MainPage = new NavigationPage(page);
                    if (initializeAsyncTask != null) await initializeAsyncTask;
                }

                IsNavigating = false;
            }

        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if(viewModelType.FullName == null)
                throw new Exception("ViewModelType is null");
            
            string viewName = (viewModelType.FullName.Replace("ViewModels", "Views")).Replace("ViewModel", "Page");
            string viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            Type viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
        private Task<Page> CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = null;
            try
            {

                page = Activator.CreateInstance(pageType) as Page;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return null;
            }
            if (page == null)
            {
                _logger.Fatal("Page not created but exception not thrown. Please check implementation.");
                return null;
            }
            else
            {
                _logger.Info($"{page} successfully created");
                return Task.FromResult(page);
            }
        }

        internal void ClearNavigation()
        {
            _logger.Debug("Setting empty page as mainpage");
            Application.Current.MainPage = new ContentPage();
        }

        public async Task BackUntilAsync<TViewModel>() where TViewModel : BaseViewModel, new()
        {
            Type pageType = GetPageTypeForViewModel(typeof(TViewModel));
            var stack = Application.Current.MainPage.Navigation.NavigationStack;
            foreach (var page in stack)
            {
                if (pageType != page.GetType()) continue;
                while (stack.Last() != page)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                return;
            }
            throw new Exception($"Page of type: {pageType} not found on navigation stack");
        }
    }
}
