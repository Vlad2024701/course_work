using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using AutoParking.Models;
using AutoParking.Services;

namespace DbFiller
{
	internal class Program
	{
		private const int placesAmt = 25;

		private static SqlClient db;
		private static List<UserCar> usersCars;

		private static void Main(string[] args)
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

		private static void InsertPlaces()
		{
			for (int i = 0; i < placesAmt; i++)
				db.Places.Add(new Place());

			db.SaveChanges();
		}

		private static void InsertAdmins()
		{
			db.Accounts.Add(new Admin("Vlad", UserManager.HashPassword("123456"), "vlad@gmail.com", "Симакович", "Владислав", "Витальевич"));
			db.Accounts.Add(new Admin("Roman", UserManager.HashPassword("123456"), "roman@gmail.com", "Заяц", "Роман", "Витальевич"));
			db.Accounts.Add(new Admin("Kirill", UserManager.HashPassword("123456"), "kirill@gmail.com", "Олешкевич", "Кирилл", "Вадимович"));

			db.SaveChanges();
		}

		private static void InsertUsers()
		{
			db.Accounts.AddRange(new List<User>()
			{
				new User("Darya", UserManager.HashPassword("password1"), "email1@gmail.com", "Иванова", "Дарья", "Ивановна"),
				new User("Valya", UserManager.HashPassword("password2"), "email2@gmail.com", "Ермакович", "Валентин", "Игоревич"),
				new User("Sanya", UserManager.HashPassword("password3"), "email3@gmail.com", "Гордеенков", "Александр", "Сергеевич"),
				new User("Petr", UserManager.HashPassword("password4"), "email4@gmail.com", "Петрович", "Петр", "Петров"),
				new User("Vitali", UserManager.HashPassword("password5"), "email5@gmail.com", "Адасько", "Виталий", "Анатольевич"),
				new User("Daniil", UserManager.HashPassword("password6"), "email6@gmail.com", "Михалкевич", "Даниил", "Ильич"),
				new User("Yana", UserManager.HashPassword("password7"), "email7@gmail.com", "Синявская", "Яна", "Павловна"),
				new User("Oleg", UserManager.HashPassword("password8"), "email8@gmail.com", "Устьян", "Олег", "Константинович"),
				new User("Vasya", UserManager.HashPassword("password9"), "email9@gmail.com", "Зуева", "Василина", "Акакьевна"),
			});

			db.SaveChanges();

			var rndm = new Random();
			usersCars = SqlClient.GetUsers()
				.Select(user =>
					new UserCar(user, $"{rndm.Next(0, 10000).ToString().PadLeft(4, '0')}AB-7"))
				.ToList();
		}

		private static void InsertBookings()
		{
			var bookings = Simulation.Simulate(SqlClient.GetUsers(), db.Places.ToList(), DateTime.Now.AddDays(-30));

			db.Bookings.AddRange(bookings);
			db.SaveChanges();
		}

		private static class Simulation
		{
			private static readonly Random rndm = new Random();

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

						newBooking = new Booking(user, place, i, DateTime.Now, usersCars.Find(uc => uc.User == user).Car);

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

			private static bool Roll(double chance) => rndm.Next(0, 101) <= chance;
		}

		private struct UserCar
		{
			public User User;
			public string Car;

			public UserCar(User user, string car)
			{
				User = user;
				Car = car;
			}
		}
	}
}