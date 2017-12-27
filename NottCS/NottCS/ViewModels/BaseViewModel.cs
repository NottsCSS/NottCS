using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

using NottCS.Services;

namespace NottCS.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        
        string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Sets a property, then tell the application that the property is changed such that it can re-render
        /// To be used in property's set method, e.g. set => SetProperty(ref _backingStore, value);
        /// Have to be used when non-user input field changed, e.g. change in Label's text, change in ActivityIndicator's bool
        /// </summary>
        /// <typeparam name="T">generic type parameter, can be auto deduced from backingStore</typeparam>
        /// <param name="backingStore">private backing field of the property</param>
        /// <param name="value">value from set method of property</param>
        /// <param name="propertyName">unused for now</param>
        /// <param name="onChanged">unused for now</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// Have to be overriden by derived classes that needs navigationData during navigation
        /// Overriden method needs to typecheck navigationData, and then process the data
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
