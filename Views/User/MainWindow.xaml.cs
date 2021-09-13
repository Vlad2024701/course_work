using System;
using System.Windows;
using System.Windows.Media.Imaging;
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
			Uri iconUri = new Uri(@"D:\2 курс\4 семестр\Курсач на сдачу\AutoParking\bin\Debug\images\car_23773.ico", UriKind.RelativeOrAbsolute);
			this.Icon = BitmapFrame.Create(iconUri);
			(DataContext as MainViewModel).CloseRequest += (sender, e) => Close();
		}
	}
}