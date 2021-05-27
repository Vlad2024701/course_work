using System.Collections.Generic;
using System.Linq;

using AutoParking.Commands;
using AutoParking.Models;
using AutoParking.Services;

namespace AutoParking.ViewModels
{
	internal class PlacesInfoViewModel : ViewModel
	{
		private List<Place> _places;

		public List<Place> Places
		{
			get { return _places; }
			set { SetProperty(ref _places, value); }
		}

		public PlacesInfoViewModel()
		{
			Places = SqlClient.GetInstance().Places.ToList();

			OpenBookingCommand.BookingComplete += () => OnPropertyChanged("Places");
		}
	}
}