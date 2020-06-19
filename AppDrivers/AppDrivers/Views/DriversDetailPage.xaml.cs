using AppDrivers.Models;
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
    public partial class DriversDetailPage : ContentPage
    {
        public DriversDetailPage()
        {
            InitializeComponent(); 
            BindingContext = new DriversDetailViewModel();
        }

        public DriversDetailPage(DriverModel driver)
        {
            InitializeComponent();

            BindingContext = new DriversDetailViewModel(driver);
        }
    }
}