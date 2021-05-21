using System.Windows;
using System.Windows.Controls;

using AutoParking.Views.Admin;

namespace AutoParking.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
    {
		public static AdminWindow Instance { get; private set; }
		public static Pages CurrentPage { get; private set; }

        public AdminWindow()
        {
			Instance = this;
            InitializeComponent();

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
