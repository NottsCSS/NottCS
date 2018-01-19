using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xunit;
using NottCS.ViewModels;
using Xamarin.Forms;

namespace NottCSTest
{
    public class ViewModelTest
    {
        //   
//        [Theory]
        public void ViewModelNavigationRequirements(Type viewModelType)
        {
            string viewName = (viewModelType.FullName.Replace("ViewModels", "Views")).Replace("ViewModel", "Page");
            string viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            Type pageType = Type.GetType(viewAssemblyName);

            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }


        }

        [Fact]
        public void AllViewModelTest()
        {
            Type baseViewModelType = typeof(BaseViewModel);
            string nspace = "NottCS.ViewModels";

            var viewModelTypes = from t in baseViewModelType.Assembly.GetTypes()
                where t.IsClass && t.Namespace == nspace && baseViewModelType.IsAssignableFrom(t) && t!= baseViewModelType
                select t;
//            q.ToList().ForEach(t => Console.WriteLine(t.Name));
            List<Type> viewModelTypeList = viewModelTypes.ToList();
            foreach (var viewModelType in viewModelTypeList)
            {
                ViewModelNavigationRequirements(viewModelType);
            }



        }
        

    }
}