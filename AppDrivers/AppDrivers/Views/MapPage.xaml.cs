using AppDrivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppDrivers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage(DriverModel driverSelected)
        {
            InitializeComponent();

            MapTrip.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(Convert.ToDouble(driverSelected.ActualPosition.Latitude), Convert.ToDouble(driverSelected.ActualPosition.Longitude)),
                   Distance.FromMiles(.5)
           ));

            MapTrip.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = driverSelected.Name,
                    Position = new Position(double.Parse(driverSelected.ActualPosition.Latitude), double.Parse(driverSelected.ActualPosition.Longitude))
                }
            );

            //datos del viaje
            Name.Text = driverSelected.Name;
            Status.Text = driverSelected.Status;
        }
    }
}