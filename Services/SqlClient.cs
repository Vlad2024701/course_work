using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

using AutoParking.Model;

namespace AutoParking.Services
{
	class SqlClient : DbContext
    {
        const int placesAmt = 25;

        public const string connectionString = "Data Source=desktop-dqpufti;Initial Catalog=AutoParking;Integrated Security=True";

        #region Connection

        private static readonly SqlConnection connection;

        static SqlClient()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlClient>());
        }

        #endregion


        #region Singleton

        private static SqlClient _instance;

        public static SqlClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlClient();
                if (_instance.Places.Count() == 0)
                    _instance.FillDb();
            }
            return _instance;
        }

        private SqlClient() : base(connection, true) { }

        #endregion


        #region Tables

        public DbSet<Auto> Auto { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion


        #region Methods

        public static User GetUserByAuto(Auto auto) => 
            GetInstance().Users.Where(user => user.Autos.Any(a => a.Number == auto.Number)).FirstOrDefault();

        #endregion

        private void FillDb()
        {
            var db = GetInstance();

            for (int i = 0; i < placesAmt; i++) db.Places.Add(new Place());

            db.SaveChanges();
        }
    }
}
