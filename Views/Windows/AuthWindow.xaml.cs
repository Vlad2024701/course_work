using System.Windows;
using System.Windows.Media;

using AutoParking.Services;

namespace AutoParking.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для AuthWindow.xaml
	/// </summary>
	public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();

        }

        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            DragMove();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBox_Login.Text.Trim(); //trim удаляет лишние пробелы до и после строки
            string password = PassBox1.Password.Trim();

            if (login.Length < 4 || login.Length > 35)
            {
                textBox_Login.ToolTip = "Длина логина от 4 до 35 символов";
                textBox_Login.Background = Brushes.PaleVioletRed;
            }
            else if (password.Length < 4 || password.Length > 35)
            {
                PassBox1.ToolTip = "Длина пароля от 4 до 35 символов";
                PassBox1.Background = Brushes.PaleVioletRed;
            }
            else
            {
                textBox_Login.ToolTip = null;
                textBox_Login.Background = Brushes.Transparent;
                PassBox1.ToolTip = null;
                PassBox1.Background = Brushes.Transparent;

                if (UserManager.Login(login, password))
                {
                    Window window;
                    if (UserManager.AccountType == AccountType.Admin)
                        window = new AdminWindow();
                    else
                        window = new UserWindow();

                    Application.Current.MainWindow = window;
                    Close();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации");
                }
            }
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            Application.Current.MainWindow = window;
            Close();
            double screeHeight = SystemParameters.FullPrimaryScreenHeight;
            double screeWidth = SystemParameters.FullPrimaryScreenWidth;
            window.Top = (screeHeight - this.Height) / 2;
            window.Left = (screeWidth - this.Width) / 2;
            window.Width = 600;
            window.Height = 500;
            window.Show();
        }
    }
}
