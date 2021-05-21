using AutoParking.Model;
using AutoParking.Services;

using System.Collections.Generic;
using System.Linq;

namespace AutoParking.ViewModels
{
	class PlacesInfoViewModel : ViewModel
    {
        private List<Place> _places;
        public List<Place> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

        public PlacesInfoViewModel()
        {
            Places = SqlClient.GetInstance().Places.ToList();
        }
    }
}
