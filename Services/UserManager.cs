using AutoParking.Model;

using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AutoParking.Services
{
	public static class UserManager
	{
		public static Account CurrentUser { get; private set; }
		public static AccountType AccountType { get; private set; }

		static UserManager()
		{
			//Login("Vlad", "123456");
			//if (CurrentUser == null)
			//{
			//	Register("Vlad", "123456", "vlad@gmail.com", "Симакович", "Владислав", "Витальевич");
			//	Login("Vlad", "123456");
			//}
			//AccountType = AccountType.None;
		}

		public static bool Login(string login, string password)
		{
			if (CurrentUser != null)
			{
				CurrentUser = null;
				AccountType = AccountType.None;
			}

			var hashedPassword = HashPassword(password);

			var user = SqlClient.GetInstance().Accounts.ToList()
				.Find(item => item.Login == login && item.Password == hashedPassword);

			if (user == null)
				return false;

			CurrentUser = user;
			AccountType = user.AccountType;
			return true;
		}

		public static (bool result, string error) Register(string login, string password, string email, string surname, string name, string middleName)
		{
			try
			{
				var db = SqlClient.GetInstance();

				if (db.Accounts.Any(item => item.Login == login))
					return (false, "Логин уже занят"); ;

				if (db.Accounts.Any(item => item.EMail == email))
					return (false, "Почта уже занята");

				var hashedPassword = HashPassword(password);

				db.Accounts.Add(new User(login, hashedPassword, email, surname, name, middleName));
				db.SaveChanges();

				return (true, null);
			}
			catch
			{
				return (false, "Не удалось зарегистрироваться");
			}
		}

		private static string HashPassword(string password)
		{
			var hashBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

			string hash = string.Empty;
			foreach (var item in hashBytes)
				hash += string.Format("{0:x2}", item);

			return hash;
		}

		public static void Logout() => CurrentUser = null;
	}

	public enum AccountType
	{
		Admin,
		User,
		None
	}
}
