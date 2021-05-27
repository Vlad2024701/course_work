using System.Collections.Generic;
using System.Windows.Input;

using AutoParking.Commands;
using AutoParking.Models;
using AutoParking.Services;

namespace AutoParking.ViewModels
{
	internal class UsersInfoViewModel : ViewModel
	{
		private string _query = string.Empty;
		private List<User> _searchResult;

		public UsersInfoViewModel()
		{
			SearchCommand = new RelayCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
			Search();
		}

		public string Query
		{
			get => _query.Trim();
			set => SetProperty(ref _query, value);
		}

		public ICommand SearchCommand { get; set; }

		public List<User> SearchResult
		{
			get => _searchResult;
			set => SetProperty(ref _searchResult, value);
		}

		private static bool CanSearchCommandExecute(object p) => true;

		private void OnSearchCommandExecuted(object p) => Search();

		private void Search()
		{
			SearchResult = SqlClient.SearchUsers(Query);
		}
	}
}