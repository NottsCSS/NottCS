using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xunit;
using NottCS.ViewModels;

namespace NottCSTest
{
    public class ViewModelTest
    {
        private static void ViewModelNavigationRequirements(Type viewModelType)
        {
            string viewName = (viewModelType.FullName.Replace("ViewModels", "Views")).Replace("ViewModel", "Page");
            string viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            Type pageType = Type.GetType(viewAssemblyName);

            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            try
            {
                Activator.CreateInstance(viewModelType);
            }
            catch (Exception)
            {
                Debug.WriteLine("ViewModel creation failed, possible constructor throw");
                throw;
            }
        }

        [Fact]
        private void ViewModelNavigationTest()
        {
            Type baseViewModelType = typeof(BaseViewModel);
            const string viewModelNamespace = "NottCS.ViewModels";

            //Get all viewmodels in the viewmodel assembly
            var viewModelTypes = from t in baseViewModelType.Assembly.GetTypes()
                                 where t.IsClass && t.Namespace == viewModelNamespace && baseViewModelType.IsAssignableFrom(t) && t != baseViewModelType
                                 select t;
            List<Type> viewModelTypeList = viewModelTypes.ToList();

            foreach (Type viewModelType in viewModelTypeList)
            {
                ViewModelNavigationRequirements(viewModelType);
            }



        }


    }
}