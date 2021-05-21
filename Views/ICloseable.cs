using System;

namespace AutoParking.ViewModels
{
	interface ICloseable
	{
		event EventHandler CloseRequest;
	}
}
