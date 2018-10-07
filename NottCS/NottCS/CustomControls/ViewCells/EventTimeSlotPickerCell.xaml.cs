using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventTimeSlotPickerCell : ViewCell
	{
        public static readonly BindableProperty DeleteCommandProperty = 
            BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(EventTimeSlotPickerCell));

        public static readonly BindableProperty TitleProperty = 
            BindableProperty.Create(nameof(Title), typeof(string), typeof(EventTimeSlotPickerCell), "Title");

        public ICommand DeleteCommand
        {
            get => (ICommand) GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

	    public string Title
	    {
	        get => (string) GetValue(TitleProperty);
	        set => SetValue(TitleProperty, value);
	    }
		public EventTimeSlotPickerCell ()
		{
			InitializeComponent ();
		}
	}
}