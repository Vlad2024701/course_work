using System.Windows;

namespace AutoParking.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для UserWindow.xaml
	/// </summary>
	public partial class UserWindow : Window
    {

        public UserWindow()
        {
            InitializeComponent();

            //Place1.Background = Color(1);
        }

        //Brush Color(int Id)
        //{
        //    return ;
        //}

        private void Back_On_Auth(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }
    }
}
