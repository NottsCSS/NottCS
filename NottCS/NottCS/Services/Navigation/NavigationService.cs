using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NottCS.ViewModels;
using NottCS.Views;
using Xamarin.Forms;

namespace NottCS.Services.Navigation
{
    internal class NavigationService : INavigationService
    {
        private readonly ILogger _logger;
        public NavigationService(ILogger logger)
        {
            _logger = logger;
        }

        private bool IsNavigating { get; set; }

        /// <summary>
        /// Navigates using generic, preferred way of navigation due to compile time type checks
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="navigationParameter">parameter to be passed during navigation</param>
        /// <returns></returns>
        public async Task NavigateToAsync<TViewModel>(object navigationParameter = null) where TViewModel : BaseViewModel, new()
        {
            await NavigateToAsync(typeof(TViewModel), navigationParameter);
        }

        /// <summary>
        /// Navigates by type, not preferred due to lack of type safety
        /// </summary>
        /// <param name="viewModelType">The type of ViewModel to navigate to</param>
        /// <param name="navigationParameter">parameter to be passed during navigation</param>
        /// <returns></returns>
        public async Task NavigateToAsync(Type viewModelType, object navigationParameter = null)
        {
            if (IsNavigating)
                return;
            IsNavigating = true;
            try
            {
                var createPageTask = Task.Run(() => CreatePage(viewModelType));

                if (viewModelType == null || !viewModelType.IsSubclassOf(typeof(BaseViewModel)))
                    throw new Exception("Cannot navigate to ViewModel that does not inherit BaseViewModel");
                if (!(Application.Current.MainPage is MainPage mainPage))
                    throw new Exception("Application MainPage is not set to NottCS.Views.MainPage");
                if (!(mainPage.Detail is NavigationPage navigationPage))
                    throw new Exception("Detail of MainPage is not NavigationPage");

                //Create the Page
                Page page = await createPageTask;
                if (page == null)
                    throw new Exception("Page unable to be created but no exception was thrown previously. Please check implementation");

                //Pass data into the ViewModel
                if (!(page.BindingContext is BaseViewModel viewModel))
                    throw new Exception($"BindingContext of {page.GetType()} does not inherit BaseViewModel");
                Task initializeAsyncTask = viewModel.InitializeAsync(navigationParameter);


                //Push the Page into the current Navigation stack
                var previousPageType = navigationPage.CurrentPage.GetType();
                Task pushPageTask = navigationPage.Navigation.PushAsync(page);

                _logger.Debug($"Previous page is: {previousPageType}");
                _logger.Info($"Pushing {page} to navigation stack");
                await pushPageTask;
                await initializeAsyncTask;
                _logger.Info($"Navigation to {page} completed successfully");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _logger.Info("Navigation terminated prematurely");
            }
            finally
            {
                IsNavigating = false;
            }

        }

        /// <summary>
        /// Sets main page using generics, preferred way of navigation due to compile time type checks
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="navigationParameter">parameter to be passed during navigation</param>
        /// <returns></returns>
        public async Task SetMainPageAsync<TViewModel>(object navigationParameter = null) where TViewModel : BaseViewModel, new()
        {
            await SetMainPageAsync(typeof(TViewModel), navigationParameter);
        }
        public async Task SetMainPageAsync(Type viewModelType, object navigationParameter = null)
        {
            if (IsNavigating)
                return;
            IsNavigating = true;
            try
            {
                var createPageTask = Task.Run(() => CreatePage(viewModelType));

                if (viewModelType == null || !viewModelType.IsSubclassOf(typeof(BaseViewModel)))
                    throw new Exception("Cannot navigate to ViewModel that does not inherit BaseViewModel");

                if (!(Application.Current.MainPage is MainPage mainPage))
                    throw new Exception("Application MainPage is not set to NottCS.Views.MainPage");

                //Create the Page
                Page page = await createPageTask;
                if (page == null)
                    throw new Exception("Page unable to be created but no exception was thrown previously. Please check implementation");

                //Pass data into the ViewModel
                if (!(page.BindingContext is BaseViewModel viewModel))
                    throw new Exception($"BindingContext of {page.GetType()} does not inherit BaseViewModel");
                Task initializeAsyncTask = viewModel.InitializeAsync(navigationParameter);

                //Set the Page as the main page
                mainPage.Detail = new NavigationPage(page);

                await initializeAsyncTask;
                _logger.Info($"Navigation to {page} completed successfully");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _logger.Info("Navigation terminated prematurely");
            }
            finally
            {
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
        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = null;
            try
            {
               Device.BeginInvokeOnMainThread(()=> page = Activator.CreateInstance(pageType) as Page);
                //Ensure that create page is run on main thread, even when the rest is ran on background
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
                return page;
            }
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
