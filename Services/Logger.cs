using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParking.Services
{
	class Logger
	{
		public const string fileName = "autoparking.log";

		public static void Log(string msg)
		{
			using (var sw = new StreamWriter(fileName))
			{
				sw.WriteLine($"{DateTime.Now:G}: {msg}");
			}
		}

		public static void Log(Exception ex)
		{
			using (var sw = new StreamWriter(fileName))
			{
				sw.WriteLine($"{DateTime.Now:G}: {ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
			}
		}
	}
}
