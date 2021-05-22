using AutoParking.Models;
using AutoParking.Services;

using System.Collections.Generic;

namespace AutoParking.ViewModels
{
	class UsersInfoViewModel : ViewModel
    {
        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public UsersInfoViewModel()
        {
            Users = SqlClient.GetUsers();
        }
    }
}
