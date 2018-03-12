using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputContent : ContentView
	{
	    public static readonly BindableProperty EventParameterListProperty =
	        BindableProperty.Create(nameof(EventParameterList), typeof(bool), typeof(ValidatableEntry), true);

	    public ObservableCollection<EventAdditionalParameter> EventParameterList
	    {
	        get => (ObservableCollection<EventAdditionalParameter>)GetValue(EventParameterListProperty);
	        set => SetValue(EventParameterListProperty, value);
	    }

        public InputContent ()
		{
			InitializeComponent ();
		}

	}
}