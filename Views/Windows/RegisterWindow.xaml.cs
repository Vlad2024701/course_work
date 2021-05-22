using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

using AutoParking.Services;

namespace AutoParking.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
    {

        public RegisterWindow()
        {
            InitializeComponent();

        }

        private bool CheckFields()
        {
            string login = textBox_Login.Text.Trim();
            string pass1 = PassBox1.Password.Trim();
            string pass2 = PassBox2.Password.Trim();
            string email = textBox_Email.Text.Trim().ToLower();
            string name = textBox_Name.Text.Trim();
            string surname = textBox_Surname.Text.Trim();
            string middle = textBox_MiddleName.Text.Trim();
            Regex emailRegex = new Regex(@"^([a-z\d\.-]+)@([a-z\d-]+)((\.([a-z]){2,4}])+)$", RegexOptions.IgnoreCase);
            //Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            Regex nameRegex = new Regex(@"^([А-я]+|[A-z]){2,30}$");
            bool pass = true;

            if (string.IsNullOrEmpty(name) && !nameRegex.IsMatch(name))
            {
                textBox_Name.ToolTip = "Поле не заполнено(2-30 символов) либо введены запрещенные символы";
                textBox_Name.Background = Brushes.PaleVioletRed;
                pass = false;
            }

            else if (string.IsNullOrEmpty(surname) && !nameRegex.IsMatch(name))
            {
                textBox_Surname.ToolTip = "Поле не заполнено(2-30 символов) либо введены запрещенные символы";
                textBox_Surname.Background = Brushes.PaleVioletRed;
                pass = false;
            }
            else if (string.IsNullOrEmpty(middle) && !nameRegex.IsMatch(middle))
            {
                textBox_MiddleName.ToolTip = "Поле не заполнено(2-30 символов) либо введены запрещенные символы";
                textBox_MiddleName.Background = Brushes.PaleVioletRed;
                pass = false;
            }
            else if (login.Length < 4 || login.Length > 35)
            {
                textBox_Login.ToolTip = "Длина логина от 4 до 35 символов";
                textBox_Login.Background = Brushes.PaleVioletRed;
                pass = false;
            }
            else if (pass1.Length < 4 || pass1.Length > 35)
            {
                PassBox1.ToolTip = "Длина пароля от 4 до 35 символов";
                PassBox1.Background = Brushes.PaleVioletRed;
                pass = false;
            }
            else if (pass1 != pass2)
            {
                PassBox2.ToolTip = "Пароли не совпадают";
                PassBox2.Background = Brushes.PaleVioletRed;
                pass = false;
            }


            else if (emailRegex.IsMatch(email))
            {
                textBox_Email.ToolTip = "Некорректный email адрес";
                textBox_Email.Background = Brushes.PaleVioletRed;
                pass = false;
            }

            return pass;
        }

        private void Register()
        {
            if (!CheckFields())
            {
                MessageBox.Show("Введите данные для регистрации!");
                return;
            }

            string login = textBox_Login.Text.Trim();
            string password = PassBox1.Password.Trim();
            string email = textBox_Email.Text.Trim().ToLower();
            string name = textBox_Name.Text.Trim();
            string surname = textBox_Name.Text.Trim();
            string middle = textBox_Name.Text.Trim();

            textBox_Login.ToolTip = null;
            textBox_Login.Background = Brushes.Transparent;
            PassBox1.ToolTip = null;
            PassBox1.Background = Brushes.Transparent;
            PassBox2.ToolTip = null;
            PassBox2.Background = Brushes.Transparent;
            textBox_Email.ToolTip = null;
            textBox_Email.Background = Brushes.Transparent;
            textBox_Name.ToolTip = null;
            textBox_Name.Background = Brushes.Transparent;
            textBox_Surname.ToolTip = null;
            textBox_Surname.Background = Brushes.Transparent;
            textBox_MiddleName.ToolTip = null;
            textBox_MiddleName.Background = Brushes.Transparent;

            var respone = UserManager.Register(login, password, email, surname, name, middle);
            if (respone.result)
            {
                MessageBox.Show("Аккаунт зарегестрирован!");
            }
            else
            {
                MessageBox.Show(respone.error, "Не удалось зарегистрироваться");
            }

            Exit();
        }

        private void Exit()
        {
            var window = new AuthWindow();
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

        private void Border_MouseDown(object sender, RoutedEventArgs e) => DragMove();

        private void Button_Reg_Click(object sender, RoutedEventArgs e) => Register();

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            var window = new AuthWindow();
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
