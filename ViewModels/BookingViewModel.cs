using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using AutoParking.Commands;
using AutoParking.Models;
using AutoParking.Services;

namespace AutoParking.ViewModels
{
	public class BookingViewModel : ViewModel, ICloseable, IDataErrorInfo
	{
		public event EventHandler CloseRequest;

		public static Place InitialPlace { get; set; }

		public Place Place { get; }

		private string _hours = string.Empty;
		public string Hours
		{
			get { return _hours.Replace('.', ',').Trim(); }
			set
			{
				if (double.TryParse(value, out double hours))
					Sum = hours * Booking.pricePerHour;

				SetProperty(ref _hours, value);
			}
		}

		private string _carNumber = string.Empty;
		public string CarNumber
		{
			get { return _carNumber.Trim(); }
			set { SetProperty(ref _carNumber, value); }
		}

		private double _sum;
		public double Sum
		{
			get { return _sum; }
			set { SetProperty(ref _sum, value); }
		}

		public List<string> CarsHistory { get; }

		public string SelectedCar { set => CarNumber = value?? string.Empty; }

		public string Error => throw new NotImplementedException();

		public string this[string columnName] => Validate(columnName);


		public ICommand BookPlaceCommand { get; }

		private void OnBookPlaceCommandExecuted(object p) => BookPlace();

		private bool CanBookPlaceCommandExecute(object p) => true;

		public BookingViewModel()
		{
			if (InitialPlace == null)
				throw new InvalidOperationException();

			BookPlaceCommand = new RelayCommand(OnBookPlaceCommandExecuted, CanBookPlaceCommandExecute);

			CarsHistory = SqlClient.GetInstance().Bookings.ToList()
				.Where(booking => booking.User == UserManager.CurrentUser as User)
				.Select(booking => booking.CarNumber).Distinct().ToList();
				
			Place = InitialPlace;
			InitialPlace = null;
		}

		private void BookPlace()
		{
			var errors = Check();
			if (!string.IsNullOrEmpty(errors))
			{
				MessageBox.Show(errors, "Не удалось забронировать место");
				return;
			}

			SqlClient.GetInstance().Bookings.Add(
				new Booking(UserManager.CurrentUser as User, Place,
								  DateTime.Now, DateTime.Now.AddHours(Convert.ToDouble(Hours)),_carNumber));

			SqlClient.GetInstance().SaveChanges();

			MessageBox.Show("Место успешно забронировано"); 
			CloseRequest?.Invoke(this, EventArgs.Empty);
		}

		private string Check()
		{
			string errors = string.Empty;

			errors += Validate("Hours") + "\n";
			errors += Validate("CarNumber") + "\n";

			return errors.Trim();
		}

		private string Validate(string columnName)
		{
			if (columnName == null)
				return string.Empty;

			string error = string.Empty;

			switch (columnName)
			{
				case "Hours":
					{
						if (string.IsNullOrEmpty(Hours))
							return "Введите время стоянки";

						if (!double.TryParse(Hours, out double value))
							return "Введите число";
						else if (value < 0.5)
							return "Время стоянки должно быть более чем пол часа";
					}
					break;
				case "CarNumber":
					{
						if (string.IsNullOrEmpty(CarNumber))
							return "Введите номер авто";

						var (isValid, forbiddenSymbols) = Validator.Validate(CarNumber, Validator.CarRegex);

						if (!isValid)
							return "Введённая строка не является гос номером";
					}
					break;
			}

			return error;
		}
	}
}
