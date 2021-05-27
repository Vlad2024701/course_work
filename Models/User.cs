using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using AutoParking.Services;

namespace AutoParking.Models
{
	public class User : Account
	{
		public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

		[NotMapped]
		public List<string> Cars => SqlClient.GetInstance().Bookings.ToList()
			.Where(booking => booking.User == this)
			.Select(booking => booking.CarNumber).Distinct().ToList();

		[NotMapped]
		public string CarsStr => string.Join(" ", Cars).Trim();

		public User() : base(AccountType.User)
		{
		}

		public User(string login, string password, string eMail, string surname, string name, string middleName)
			: base(login, password, eMail, surname, name, middleName, AccountType.User)
		{ }

		public override string ToString() => $"{Id} {FullName} {Login} {EMail} {CarsStr}";
	}
}