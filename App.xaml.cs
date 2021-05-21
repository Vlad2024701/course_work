using System.Windows;

using AutoParking.Services;

namespace AutoParking
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Logger.Log(e.Exception);
		}
	}
}
