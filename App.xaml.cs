using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

using AutoParking.Services;

namespace AutoParking
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			InitCulture();
		}

		private void InitCulture()
		{
			var vCulture = new CultureInfo("ru-BY");

			Thread.CurrentThread.CurrentCulture = vCulture;
			Thread.CurrentThread.CurrentUICulture = vCulture;
			CultureInfo.DefaultThreadCurrentCulture = vCulture;
			CultureInfo.DefaultThreadCurrentUICulture = vCulture;

			FrameworkElement.LanguageProperty.OverrideMetadata(
			typeof(FrameworkElement),
			new FrameworkPropertyMetadata(
		 XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			SqlClient.GetInstance().Dispose();
			Logger.Log(e.Exception);
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			SqlClient.GetInstance().Dispose();
		}
	}
}