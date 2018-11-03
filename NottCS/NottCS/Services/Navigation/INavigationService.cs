using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.ViewModels;

namespace NottCS.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(object navigationParameter = null) where TViewModel : BaseViewModel, new();
        Task NavigateToAsync(Type viewModelType, object navigationParameter = null);
        Task BackUntilAsync<TViewModel>() where TViewModel : BaseViewModel, new();
    }
}