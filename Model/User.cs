using System.Collections.Generic;

namespace AutoParking.Model
{
	public class User : AccountBase
    {
        public virtual List<Auto> Autos { get; set; } = new List<Auto>();

        public User() : base(Services.AccountType.User) { }

        public User(string login, string password, string eMail, string name, string surname, string middleName)
            : base(login, password, eMail, name, surname, middleName, Services.AccountType.User) { }
    }
}
