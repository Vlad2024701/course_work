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
		static List<UserCar> usersCars;

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
				new User("login1", UserManager.HashPassword("password1"), "email1@gmail.com", "Фамилия1", "Имя1", "Отчество1"),
				new User("login2", UserManager.HashPassword("password2"), "email2@gmail.com", "Фамилия2", "Имя2", "Отчество2"),
				new User("login3", UserManager.HashPassword("password3"), "email3@gmail.com", "Фамилия3", "Имя3", "Отчество3"),
				new User("login4", UserManager.HashPassword("password4"), "email4@gmail.com", "Фамилия4", "Имя4", "Отчество4"),
				new User("login5", UserManager.HashPassword("password5"), "email5@gmail.com", "Фамилия5", "Имя5", "Отчество5"),
				new User("login6", UserManager.HashPassword("password6"), "email6@gmail.com", "Фамилия6", "Имя6", "Отчество6"),
				new User("login7", UserManager.HashPassword("password7"), "email7@gmail.com", "Фамилия7", "Имя7", "Отчество7"),
				new User("login8", UserManager.HashPassword("password8"), "email8@gmail.com", "Фамилия8", "Имя8", "Отчество8"),
				new User("login9", UserManager.HashPassword("password9"), "email9@gmail.com", "Фамилия9", "Имя9", "Отчество9"),
			});

			db.SaveChanges();

			var rndm = new Random();
			usersCars = SqlClient.GetUsers()
				.Select(user => 
					new UserCar(user, $"{rndm.Next(0, 10000).ToString().PadLeft(4, '0')}AB-7"))
				.ToList();
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

			static bool Roll(double chance) => rndm.Next(0, 101) <= chance;
		}

		struct UserCar
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
