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
                if (viewModelType == null || !viewModelType.IsSubclassOf(typeof(BaseViewModel)))
                {
                    Debug.WriteLine("passed viewmodel type does not inherit BaseViewModel");
                    _isNavigating = false;
                    return;
                }

                Page page = null;
                try
                {
                    page = await CreatePage(viewModelType);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.TargetSite);
                }
                if (Application.Current.MainPage is NavigationPage navigationPage && page!=null )
                {
                        if (page.BindingContext is BaseViewModel viewModel)
                        {
                            await viewModel.InitializeAsync(navigationParameter);
                            Debug.WriteLine($"Previous page is: {navigationPage.CurrentPage}");
                            Debug.WriteLine($"Now navigating to:{page}");
                            await navigationPage.PushAsync(page);
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
            Page page = Activator.CreateInstance(pageType) as Page;
            return Task.FromResult(page);
        }
    }
}
