using AppDrivers.Models;
using AppDrivers.Services;
using AppDrivers.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AppDrivers.ViewModels
{
    public class DriversViewModel : BaseViewModel
    {
        ObservableCollection<DriverModel> drivers;

        static DriversViewModel _instance;

        Command _newCommand;
        public Command NewCommand => _newCommand ?? (_newCommand = new Command(NewAction));

        Command<DriverModel> _modifyCommand;
        public Command<DriverModel> ModifyCommand => _modifyCommand ?? (_modifyCommand = new Command<DriverModel>(ModifyAction));

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(RefreshAction));

        public ObservableCollection<DriverModel> Drivers
        {
            get => drivers;
            set => SetProperty(ref drivers, value);
        }

        public DriversViewModel()
        {
            _instance = this;

            LoadDrivers();
        }

        private async void LoadDrivers()
        {
            //sabemos que se esta procesando algo en las vistas
            IsBusy = true;

            ApiResponse response = await new ApiService().GetDataAsync<DriverModel>("driver");
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("No cargó compa", response.Message, "Arre");
                return;
            }
            //convertir 
            Drivers = response.Result as ObservableCollection<DriverModel>;
            IsBusy = false;
        }
        public static DriversViewModel GetInstance() //static, no se debe instanciar con new 
        {
            if (_instance == null) _instance = new DriversViewModel();
            return _instance;
        }

        DriverModel driverSelected;
        public DriverModel DriverSelected
        {
            get => driverSelected;
            set => SetProperty(ref driverSelected, value);
        }
        private void NewAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DriversDetailPage());
        }
        private void SelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DriversDetailPage(DriverSelected));
        }
        void ModifyAction(DriverModel driver)
        {
            if (driver != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(new DriversDetailPage(driver as DriverModel));
            }
        }
        public void RefreshAction()
        {
            LoadDrivers();
        }
    }
}
