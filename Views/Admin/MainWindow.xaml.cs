using System.Windows;
using System.Windows.Controls;

using AutoParking.ViewModels;

namespace AutoParking.Views.Admin
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
		public static MainWindow Instance { get; private set; }
		public static Pages CurrentPage { get; private set; }

        public MainWindow()
        {
			Instance = this;
            InitializeComponent();
			(DataContext as MainViewModel).CloseRequest += (sender, e) => Close();

			SwitchPage(Pages.Places);
		}

		public void Refresh() => SwitchPage(CurrentPage);

		public void SwitchPage(Pages page)
		{
			Page content;

			switch (page)
			{
				case Pages.PlacesInfo:
					content = new PlacesInfoPage();
					break;
				case Pages.Places:
					content = new PlacesPage();
					break;
				case Pages.UsersInfo:
					content = new UsersInfoPage();
					break;
				default:
					MessageBox.Show("Страница не найдена");
					content = new PlacesPage();
					break;
			}

			CurrentPage = page;
			MainContent.Content = content;
		}

		private void Places_Button_Click(object sender, RoutedEventArgs e) => SwitchPage(Pages.Places);

		private void PlacesInfo_Button_Click(object sender, RoutedEventArgs e) => SwitchPage(Pages.PlacesInfo);

		private void UsersInfo_Button_Click(object sender, RoutedEventArgs e) => SwitchPage(Pages.UsersInfo);
	}
}
