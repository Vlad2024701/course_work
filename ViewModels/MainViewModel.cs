using AutoParking.Commands;
using AutoParking.Services;
using AutoParking.Views.Windows;

using System;
using System.Windows;
using System.Windows.Input;

namespace AutoParking.ViewModels
{
	class MainViewModel : ViewModel, ICloseable
    {
		public event EventHandler CloseRequest;

		public string UserName { get => $"{UserManager.CurrentUser.Surname} {UserManager.CurrentUser.Name}"; }


		public ICommand LogoutCommand { get; set; }

		private static bool CanLogoutCommandExecute(object p) => true;

		private void OnLogoutCommandExecuted(object p) => Logout();


		public MainViewModel()
		{
			LogoutCommand = new RelayCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);
		}

		private void Logout()
		{
			UserManager.Logout();
			var window = new AuthWindow();
			CloseRequest?.Invoke(this, EventArgs.Empty);
			Application.Current.MainWindow = window;
			window.Show();
		}
	}
}
