namespace AutoParking.Model
{
	class Admin : AccountBase
    {
        public Admin() : base(Services.AccountType.Admin) { }

        public Admin(string login, string password, string eMail, string name, string surname, string middleName)
            : base(login, password, eMail, name, surname, middleName, Services.AccountType.Admin) { }
    }
}
