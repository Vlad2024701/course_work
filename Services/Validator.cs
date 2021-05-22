using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoParking.Services
{
	public static class Validator
	{
		public static Regex NameRegex = new Regex(@"^[А-Яа-яЁёA-Za-z \-]+$");
		public static Regex CarRegex = new Regex(@"^\d{4}[ABEKMHOPCTYX]{2}\-[1-7]$");

		public static (bool isValid, List<string> forbiddenSymbols) Validate(string value, Regex regex)
		{
			if (regex.IsMatch(value))
				return (true, null);

			return (false, value.ToCharArray()
				.Where(symbol => !regex.IsMatch(symbol.ToString()))
				.Select(symbol => symbol.ToString()).ToList());
		}

		public static string JoinSymbols(List<string> symbols) => "\"" + string.Join("\", \"", symbols) + "\"";
	}
}