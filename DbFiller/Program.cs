using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using AutoParking.Models;
using AutoParking.Services;

namespace DbFiller
{
	class Program
	{
		const int placesAmt = 25;

		static SqlClient db;

		static void Main(string[] args)
		{
			db = SqlClient.GetInstance(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

			InsertPlaces();
			Console.WriteLine("Места вставлены");
			InsertAdmins();
			Console.WriteLine("Админы вставлены");
			InsertUsers();
			Console.WriteLine("Пользователи вставлены");
			InsertBookings();
			Console.WriteLine("Брони вставлены");

			Console.WriteLine("Готово");
		}

		static void InsertPlaces()
		{
			for (int i = 0; i < placesAmt; i++)
				db.Places.Add(new Place());

			db.SaveChanges();
		}

		static void InsertAdmins()
		{
			db.Accounts.Add(new Admin("Vlad", UserManager.HashPassword("123456"), "vlad@gmail.com", "Симакович", "Владислав", "Витальевич"));

			db.SaveChanges();
		}

		static void InsertUsers()
		{

			db.Accounts.AddRange(new List<User>()
			{
				new User("login1", UserManager.HashPassword("password1"), "email1@gmail.com", "Фамилия1", "Имя1", "Отчество1", "1111AB-7"),
				new User("login2", UserManager.HashPassword("password2"), "email2@gmail.com", "Фамилия2", "Имя2", "Отчество2", "1122AB-7"),
				new User("login3", UserManager.HashPassword("password3"), "email3@gmail.com", "Фамилия3", "Имя3", "Отчество3", "1133AB-7"),
				new User("login4", UserManager.HashPassword("password4"), "email4@gmail.com", "Фамилия4", "Имя4", "Отчество4", "1144AB-7"),
				new User("login5", UserManager.HashPassword("password5"), "email5@gmail.com", "Фамилия5", "Имя5", "Отчество5", "1155AB-7"),
				new User("login6", UserManager.HashPassword("password6"), "email6@gmail.com", "Фамилия6", "Имя6", "Отчество6", "1166AB-7"),
				new User("login7", UserManager.HashPassword("password7"), "email7@gmail.com", "Фамилия7", "Имя7", "Отчество7", "1177AB-7"),
				new User("login8", UserManager.HashPassword("password8"), "email8@gmail.com", "Фамилия8", "Имя8", "Отчество8", "1188AB-7"),
				new User("login9", UserManager.HashPassword("password9"), "email9@gmail.com", "Фамилия9", "Имя9", "Отчество9", "1199AB-7"),
			});

			db.SaveChanges();
		}

		static void InsertBookings()
		{
			var bookings = Simulation.Simulate(SqlClient.GetUsers(), db.Places.ToList(), DateTime.Now.AddDays(-30));

			db.Bookings.AddRange(bookings);
			db.SaveChanges();
		}

		static class Simulation
		{
			static readonly Random rndm = new Random();
			private static string carNumber = "1111AB-2";

			public static List<Booking> Simulate(List<User> users, List<Place> places, DateTime initialDate)
			{
				var result = new List<Booking>();

				var currentBookings = new List<Booking>();
				var freeUsers = new List<User>(users);
				var freePlaces = new List<Place>(places);

				for (DateTime i = initialDate; i <= DateTime.Now; i = i.AddHours(1))
				{
					Booking newBooking = null;

					if (freeUsers.Count > 0 && Roll(users.Count * 10) && freePlaces.Count > 0)
					{
						var user = freeUsers[rndm.Next(0, freeUsers.Count)];
						var place = freePlaces[rndm.Next(0, freePlaces.Count)];

						newBooking = new Booking(user, place, i, DateTime.Now, carNumber);

						currentBookings.Add(newBooking);
						freeUsers.Remove(user);
						freePlaces.Remove(place);
					}

					if (currentBookings.Count > 0 && Roll(50))
					{
						var booking = currentBookings[rndm.Next(0, currentBookings.Count)];
						if (booking != newBooking)
						{
							booking.EndTime = i;
							result.Add(booking);

							currentBookings.Remove(booking);
							freeUsers.Add(booking.User);
							freePlaces.Add(booking.Place);
						}
					}
				}

				foreach (var item in currentBookings)
					item.EndTime = DateTime.Now.AddHours(rndm.Next(1, 48));

				result.AddRange(currentBookings);

				return result;
			}

			static bool Roll(double chance) => rndm.Next(0, 101) <= chance;
		}
	}
}
