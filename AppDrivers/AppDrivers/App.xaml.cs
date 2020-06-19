using AppDrivers.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDrivers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new DriversPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
