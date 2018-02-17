using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using NottCS.ViewModels;
using Xamarin.Forms;

namespace NottCS.Services.Navigation
{
    public static class NavigationService
    {
        private static bool _isNavigating = false;
        /// <summary>
        /// Navigates using viewmodel, preferred way of navigation due to type checks during compile time
        /// Calls InitializeAsync method with the passed parameter during navigation, override that method to use the parameter
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="navigationParameter">parameter to be passed during navigation</param>
        /// <returns></returns>
        internal static async Task NavigateToAsync<TViewModel>(object navigationParameter = null) where TViewModel : BaseViewModel, new()
        {
            await NavigateToAsync(typeof(TViewModel), navigationParameter);
        }

        internal static async Task NavigateToAsync(Type viewModelType, object navigationParameter = null)
        {
            if (!_isNavigating) //prevents simultaneous navigations
            {
                _isNavigating = true;

                Page page = null;
                var createPageTask = CreatePage(viewModelType);
                var initializeViewModelAsyncTask = Task.Run(async () =>
                    await InitializeBaseViewModel(viewModelType, navigationParameter));

                if (viewModelType == null || !viewModelType.IsSubclassOf(typeof(BaseViewModel)))
                {
                    Debug.WriteLine("passed viewmodel type does not inherit BaseViewModel");
                    _isNavigating = false;
                    return;
                }

                try
                {
                    page = await createPageTask;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.TargetSite);
                }
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    if (page?.BindingContext is BaseViewModel viewModel)
                    {
                        Task pushPageTask = navigationPage.PushAsync(page);
                        try
                        {
                            page.BindingContext = await initializeViewModelAsyncTask;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            Debug.WriteLine(e.TargetSite);
                        }

                        Debug.WriteLine($"Previous page is: {navigationPage.CurrentPage}");
                        Debug.WriteLine($"Now navigating to:{page}");
                        await pushPageTask;
                    }
                    else
                    {
                        Type pageType = page.GetType();
                        Debug.WriteLine($"{pageType} has binding context that is not derived from BaseViewModel");
                        _isNavigating = false;
                        return;
                    }
                }
                else
                {
                    Type pageType = Application.Current.MainPage.GetType();
                    Debug.WriteLine($"{pageType} is not navigationPage");
                }

                _isNavigating = false;
            }

        }

        internal static async Task GoBackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.PopAsync();
            }
        }

        private static Type GetPageTypeForViewModel(Type viewModelType)
        {
            string viewName = (viewModelType.FullName.Replace("ViewModels", "Views")).Replace("ViewModel", "Page");
            string viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            Type viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
        private static Task<Page> CreatePage(Type viewModelType)
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
                Debug.WriteLine($"Exception thrown at CreatePage {e}");
                Debug.WriteLine($"{e.Message}");
            }
            if (page == null)
            {
                Debug.WriteLine($"Page not created");
                return null;
            }
            else
            {
                Debug.WriteLine($"{page} successfully created");
                return Task.FromResult(page);
            }
        }

        private static async Task<BaseViewModel> InitializeBaseViewModel(Type viewModelType, object navigationParameter)
        {
            BaseViewModel viewModel = Activator.CreateInstance(viewModelType) as BaseViewModel;
            if (viewModel != null)
            {
                Debug.WriteLine($"{viewModel} successfully created");
                await viewModel.InitializeAsync(navigationParameter);
                return viewModel;
            }
            else
            {
                Debug.WriteLine("viewModel not created");
                return null;
            }
        }

        private static Task InitializeAsyncTask(Page page, object navigationData)
        {
            if (page.BindingContext is BaseViewModel viewModel)
            {
                return viewModel.InitializeAsync(navigationData);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
