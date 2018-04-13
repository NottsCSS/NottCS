using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NottCS.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// <para>Sets a property, then tells the application that the property is changed such that it can re-render</para>
        /// <para>To be used in property's set method, e.g. set => SetProperty(ref _backingStore, value);</para>
        /// <para>Have to be used when non-user input field changed, e.g. change in Label's text, change in ActivityIndicator's bool</para>
        /// </summary>
        /// <typeparam name="T">generic type parameter, can be auto deduced from backingStore</typeparam>
        /// <param name="backingStore">private backing field of the property</param>
        /// <param name="value">value from set method of property</param>
        /// <param name="onChanged">function to be called when the property changes</param>
        /// <param name="propertyName">default is good</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            Action onChanged = null,
                [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// <para>Have to be overriden by derived classes that needs navigationData during navigation</para>
        /// <para>Overriden method needs to typecheck navigationData, and then process the data</para>
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

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
