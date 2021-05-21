using System.ComponentModel.DataAnnotations;

using AutoParking.Services;

namespace AutoParking.Model
{
	public abstract class Account
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public AccountType AccountType { get; set; }

		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[EmailAddress]
		public string EMail { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string MiddleName { get; set; }

		public string FullName { get => $"{Surname} {Name} {MiddleName}"; }

		public Account(AccountType type)
		{
			Id = 0;
			AccountType = type;
		}

		protected Account(string login, string password, string eMail, string surname, string name, string middleName, AccountType type)
		{
			Id = 0;
			AccountType = type;

			Login = login;
			Password = password;
			EMail = eMail;
			Name = name;
			Surname = surname;
			MiddleName = middleName;
		}
	}
}
