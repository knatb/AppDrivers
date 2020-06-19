using AppDrivers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDrivers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriversPage : ContentPage
    {
        public DriversPage()
        {
            InitializeComponent();

            BindingContext = new DriversViewModel();
        }
    }
}