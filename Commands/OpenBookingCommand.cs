using System;

using AutoParking.Models;
using AutoParking.Services;
using AutoParking.ViewModels;
using AutoParking.Views.User;

namespace AutoParking.Commands
{
	internal class OpenBookingCommand : Command
	{
		public static event Action BookingComplete;

		public override bool CanExecute(object parameter)
		{
			if (parameter is Place place)
				return !SqlClient.GetInstance().Places.Find(place.Id).IsBooked;

			return false;
		}

		public override void Execute(object parameter)
		{
			if (parameter is Place place)
			{
				BookingViewModel.InitialPlace = place;
				var window = new BookingWindow();
				window.ShowDialog();
				BookingComplete?.Invoke();
			}
			else
				throw new InvalidOperationException();
		}
	}
}