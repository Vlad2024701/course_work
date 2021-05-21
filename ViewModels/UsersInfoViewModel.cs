using AutoParking.Model;
using AutoParking.Services;

using System.Collections.Generic;
using System.Linq;

namespace AutoParking.ViewModels
{
	class UsersInfoViewModel : ViewModel
    {
        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set => Set(ref _users, value);
        }

        public UsersInfoViewModel()
        {
            Users = SqlClient.GetInstance().Users.ToList();
        }
    }
}
