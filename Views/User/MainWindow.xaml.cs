using System.Windows;

using AutoParking.ViewModels;

namespace AutoParking.Views.User
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			(DataContext as MainViewModel).CloseRequest += (sender, e) => Close();
		}
	}
}