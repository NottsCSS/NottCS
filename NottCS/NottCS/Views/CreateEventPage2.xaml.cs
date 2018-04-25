using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.Services;
using NottCS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage2 : ContentPage
    {
        private int _k = 10;
        public CreateEventPage2()
        {
            InitializeComponent();
            BindingContext = new CreateEventViewModel2();
            var entryTemp = new Label();
            entryTemp.SetBinding(Label.TextProperty, ((CreateEventViewModel2)BindingContext).SomeString);
            entryTemp.VerticalOptions = LayoutOptions.Start;
            var children = MainStack.Children;
            children.Insert(children.Count - 1, entryTemp); // Adding to the last-1 stack to make sure button is always the last
            button1.Pressed += ButtonHandler;
        }

        private void ButtonHandler(object sender, EventArgs args)
        {
            var entryTemp = new Label() { Text = $"String {_k}", VerticalOptions = LayoutOptions.Start, FontSize = 10 };
            var children = MainStack.Children;
            children.Insert(children.Count - 1, entryTemp);
            _k++;
        }
    }
}