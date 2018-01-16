using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NottCS.ViewModels;
using Xamarin.Forms;

namespace NottCS.Services.Navigation
{
    static class NavigationService
    {
        public static async Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel, new()
        {
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
                    await navigationPage.PushAsync(page);
                }

            }
            else
            {
                Type pageType = Application.Current.MainPage.GetType();
                String pageTypeString = pageType.ToString();
                throw new Exception($"{pageTypeString} is not navigationPage");
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
