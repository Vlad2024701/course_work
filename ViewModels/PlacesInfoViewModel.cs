using AutoParking.Commands;
using AutoParking.Models;
using AutoParking.Services;

using System.Collections.Generic;
using System.Linq;

namespace AutoParking.ViewModels
{
	class PlacesInfoViewModel : ViewModel
    {
        public static PlacesInfoViewModel Instance { get; private set; }
        private List<Place> _places;

        public List<Place> Places
        {
            get { return _places; }
            set { SetProperty(ref _places, value); }
        }

        public PlacesInfoViewModel()
        {
            Instance = this;
            Places = SqlClient.GetInstance().Places.ToList();

            OpenBookingCommand.BookingComplete += () => OnPropertyChanged("Places");
        }


    }
}
