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
        internal static async Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel, new()
        {
            //if mainpage is not navigation page it will not be able to push another page onto the navigation stack
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                Page page = null;
                Type viewModelType = typeof(TViewModel);
                try
                {
                    page = CreatePage(viewModelType);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                if (page != null)
                {
                    if (page.BindingContext is BaseViewModel viewModel)
                    {
                        await viewModel.InitializeAsync(parameter);
                    }
                    else
                    {
                        Type pageType = page.GetType();
                        throw new Exception($"{pageType} has binding context that is not derived from BaseViewModel");
                    }

                    if (navigationPage.CurrentPage.GetType() != page.GetType() && !_isNavigating) //prevents navigation to same page multiple times
                    {
                        _isNavigating = true;
                        Debug.WriteLine($"Previous page is: {navigationPage.CurrentPage}");
                        Debug.WriteLine($"Now navigating to:{page}");
                        await navigationPage.PushAsync(page);
                        _isNavigating = false;
                    }
                }

            }
            else
            {
                Type pageType = Application.Current.MainPage.GetType();
                String pageTypeString = pageType.ToString();
                throw new Exception($"{pageTypeString} is not navigationPage");
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
        private static Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }
            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
