using System.Collections.Generic;

namespace AutoParking.Model
{
	public class User : Account
	{
		public virtual List<Car> Cars { get; set; } = new List<Car>();

		public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

		public User() : base(Services.AccountType.User) { }

		public User(string login, string password, string eMail, string surname, string name, string middleName, params Car[] cars)
			: base(login, password, eMail, surname, name, middleName, Services.AccountType.User)
		{
			if (cars != null)
				Cars = new List<Car>(cars);
		}
	}
}
