namespace AutoParking.Models
{
	public class Admin : Account
	{
		public Admin() : base(Services.AccountType.Admin)
		{
		}

		public Admin(string login, string password, string eMail, string surname, string name, string middleName)
			: base(login, password, eMail, surname, name, middleName, Services.AccountType.Admin) { }
	}
}