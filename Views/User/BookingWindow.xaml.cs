using System.Windows;
using System.Windows.Input;

using AutoParking.ViewModels;

namespace AutoParking.Views.User
{
	/// <summary>
	/// Логика взаимодействия для BookingWindow.xaml
	/// </summary>
	public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
            (DataContext as BookingViewModel).CloseRequest += (sender, e) => Close();
        }

		private void Hours_TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Key == Key.OemComma || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
				return;

			e.Handled = e.KeyboardDevice.Modifiers == ModifierKeys.Shift || e.Key < Key.D0 || e.Key > Key.D9;
		}
	}
}

