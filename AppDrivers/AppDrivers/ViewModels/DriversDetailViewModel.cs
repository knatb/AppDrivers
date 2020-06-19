using AppDrivers.Models;
using AppDrivers.Services;
using AppDrivers.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppDrivers.ViewModels
{
    public class DriversDetailViewModel : BaseViewModel
    {

        Command _saveCommand; //tipos de objetos que requerimos cuando se ejecuta una acción en la vista
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand; //tipos de objetos que requerimos cuando se ejecuta una acción en la vista
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(MapAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));
        public DriversDetailViewModel() { }

        int _ID;
        public int id
        {
            get => _ID;
            set => SetProperty(ref _ID, value);
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        string _Picture;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }

        string _Status;
        public string Status
        {
            get => _Status;
            set => SetProperty(ref _Status, value);
        }

        public string _Longitude;
        public string Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        string _Latitude;
        public string Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        public DriversDetailViewModel(DriverModel driver)
        {
            id = driver.IDDriver;
            Name = driver.Name;
            Status = driver.Status;
            Picture = driver.Picture;
            Latitude = driver.ActualPosition.Latitude;
            Longitude = driver.ActualPosition.Longitude;
        }

        /*
        public DriversDetailViewModel(DriverModel driver) {
            id = driver.IDDriver;
            Name = driver.Name;
            Status = driver.Status;
            Picture = driver.Picture;
            Latitude = driver.ActualPosition.Latitude;
            Longitude = driver.ActualPosition.Longitude;

        }

        int _ID;
        public int id
        {
            get => _ID;
            set => SetProperty(ref _ID, value);
        }

        string _Name = string.Empty;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value); //por ser view model, y propiedades que se "bindean" a la vista
        }

        string _Status = string.Empty;
        public string Status
        {
            get => _Status;
            set => SetProperty(ref _Status, value); //por ser view model, y propiedades que se "bindean" a la vista
        }

        string _Picture = string.Empty;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value); //por ser view model, y propiedades que se "bindean" a la vista
        }

        string _Latitude;
        public string Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        public string _Longitude;
        public string Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }
        */

        public DriverModel _Driver;
        public DriverModel Driver
        {
            get => _Driver;
            set => SetProperty(ref _Driver, value);
        }

        ImageSource _PictureSource;
        public ImageSource PictureSource
        {
            get => _PictureSource;
            set => SetProperty(ref _PictureSource, value);
        }
        private async void SaveAction() //guardar
        {
            IsBusy = true;
            //throw new NotImplementedException();
            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("driver", new DriverModel
                {
                    Name = this.Name,
                    Status = this.Status,
                    Picture = this.Picture,
                    ActualPosition = new PositionModel
                    {
                        Latitude = this.Latitude,
                        Longitude = this.Longitude
                    }

                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", "Error al crear conductor", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("driver", this.id ,new DriverModel
                {
                    IDDriver = this.id,
                    Name = this.Name,
                    Status = this.Status,
                    Picture = this.Picture,
                    ActualPosition = new PositionModel
                    {
                        Latitude = this.Latitude,
                        Longitude = this.Longitude
                    }
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", "Error al actualizar conductor", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");

            }
            IsBusy = false;

            //Application.Current.MainPage.DisplayAlert("Mensaje", "Excelsior", "Arre");
            //await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void DeleteAction()
        {
            IsBusy = true;
            
                ApiResponse response = await new ApiService().DeleteDataAsync("driver", id);
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", "Error al eliminar conductor", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("AppAzureSQL", response.Message, "Ok");
            
        }
        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new MapPage(new DriverModel
            {
                IDDriver = this.id,
                Name = this.Name,
                Status = this.Status,
                Picture = this.Picture,
                ActualPosition = new PositionModel
                {
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                }
            }));
        }
        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {

                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        private async void TakePictureAction()
        {
            //await CrossMedia.Current.Initialize(); esto se hace en el main activity de android

            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null) //si el archivo es nulo, se sale
                return;

            Driver.Picture = file.Path; //se actualiza la imagen en memoria de la que habia martillada
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK"); //se anda el path de donde quedo la imagen

            PictureSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });
        }
        private async void SelectPictureAction(object obj)
        {
            //throw new NotImplementedException();

            if (!CrossMedia.Current.IsPickPhotoSupported) //si no hay soporte, manda un alert
            {
                await Application.Current.MainPage.DisplayAlert("Error", ":( Not supported action.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
            });

            if (file == null) //si el archivo es nulo, se sale
                return;

            Driver.Picture = file.Path; //se actualiza la imagen en memoria de la que habia martillada
            //await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK"); //se anda el path de donde quedo la imagen

            PictureSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });
        }
    }
}
