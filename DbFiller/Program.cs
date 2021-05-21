﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using AutoParking.Model;
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
			InsertCars();
			Console.WriteLine("Машины вставлены");
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
			db.Accounts.Add(new Admin("Kirill", "123", "kirill@gmail.com", "Олешкевич", "Кирилл", "Вадимович"));

			db.SaveChanges();
		}

		static void InsertUsers()
		{
			var cars = db.Cars.ToList();

			db.Accounts.AddRange(new List<User>()
			{
				new User("login1", "password1", "email1@gmail.com", "Фамилия1", "Имя1", "Отчество1", cars[0]),
				new User("login2", "password2", "email2@gmail.com", "Фамилия2", "Имя2", "Отчество2", cars[1]),
				new User("login3", "password3", "email3@gmail.com", "Фамилия3", "Имя3", "Отчество3", cars[2]),
				new User("login4", "password4", "email4@gmail.com", "Фамилия4", "Имя4", "Отчество4", cars[3]),
				new User("login5", "password5", "email5@gmail.com", "Фамилия5", "Имя5", "Отчество5", cars[4]),
				new User("login6", "password6", "email6@gmail.com", "Фамилия6", "Имя6", "Отчество6", cars[5]),
				new User("login7", "password7", "email7@gmail.com", "Фамилия7", "Имя7", "Отчество7", cars[6], cars[9]),
				new User("login8", "password8", "email8@gmail.com", "Фамилия8", "Имя8", "Отчество8", cars[7], cars[10]),
				new User("login9", "password9", "email9@gmail.com", "Фамилия9", "Имя9", "Отчество9", cars[8], cars[11]),
			});

			db.SaveChanges();
		}

		static void InsertCars()
		{
			db.Cars.AddRange(new List<Car>()
			{
				new Car("1111AB-7", "Модель1"),
				new Car("1122AB-7", "Модель1"),
				new Car("1133AB-7", "Модель2"),
				new Car("1144AB-7", "Модель2"),
				new Car("1155AB-7", "Модель3"),
				new Car("1166AB-7", "Модель3"),
				new Car("1177AB-7", "Модель3"),
				new Car("1188AB-7", "Модель4"),
				new Car("1199AB-7", "Модель5"),
				new Car("AB2200-7", "Модель6"),
				new Car("AB2211-7", "Модель7"),
				new Car("AB2222-7", "Модель8"),
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

						newBooking = new Booking(user, user.Cars[0], place, i, DateTime.Now);
						currentBookings.Add(newBooking);
					}

					if (currentBookings.Count > 0 && Roll(50))
					{
						var booking = currentBookings[rndm.Next(0, currentBookings.Count)];
						if (booking != newBooking)
						{
							booking.EndTime = i;
							result.Add(booking);
							currentBookings.Remove(booking);
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
