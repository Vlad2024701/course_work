using System;

namespace AutoParking.ViewModels
{
	internal interface ICloseable
	{
		event EventHandler CloseRequest;
	}
}