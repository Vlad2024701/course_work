using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

using AutoParking.Models;

namespace AutoParking.Services
{
	public class SqlClient : DbContext
	{
		#region Connection

		private static readonly string connectionString;

		static SqlClient()
		{
			connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlClient>());
		}

		#endregion Connection
		//DbContext: определяет контекст данных, используемый для взаимодействия с базой данных.
		//DbSet/DbSet<TEntity>: представляет набор сущностей, хранящихся в базе данных
		#region Singleton

		private static SqlClient _instance;

		public static SqlClient GetInstance()
		{ 
			if (_instance == null)
				_instance = new SqlClient();
			return _instance;
		}

		public static SqlClient GetInstance(string connectionString)
		{
			if (_instance == null)
				_instance = new SqlClient(connectionString);
			return _instance;
		}

		private SqlClient() : base(connectionString)
		{
		}

		private SqlClient(string connectionString) : base(connectionString)
		{
		}

		#endregion Singleton

		#region Tables

		public DbSet<Booking> Bookings { get; set; }

		public DbSet<Place> Places { get; set; }

		public DbSet<Account> Accounts { get; set; }

		#endregion Tables

		#region Methods

		public static List<User> GetUsers() => GetInstance().Accounts
			.Where(account => account.AccountType == AccountType.User)
			.Select(account => account as User).ToList();

		public static List<Place> GetOccupiedPlaces(DateTime time) => GetInstance().Places
			.Where(place => !place.Bookings.Any(booking => booking.StartTime < time && booking.EndTime > time))
			.ToList();

		public static List<User> SearchUsers(string query)
		{
			query = query?.ToLower();

			if (!string.IsNullOrEmpty(query))
			{
				var tags = query.Split(' ');

				return
					GetUsers().Where(booking =>
						tags.All(tag => booking.ToString().ToLower().Contains(tag))).ToList();
			}
			else
				return GetUsers();
		}

		#endregion Methods
	}
}