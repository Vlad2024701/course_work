using System.Collections.Generic;

namespace AutoParking.Models
{
	public class User : Account
	{
		public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

		public User() : base(Services.AccountType.User) { }

		public User(string login, string password, string eMail, string surname, string name, string middleName)
			: base(login, password, eMail, surname, name, middleName, Services.AccountType.User)
		{ }
	}
}
