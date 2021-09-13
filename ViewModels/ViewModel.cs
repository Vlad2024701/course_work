using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoParking.ViewModels
{
	//Этот интерфейс позволяет свойству объекта уведомлять связанный элемент управления об изменении свойства, 
	//чтобы элемент управления мог отображать обновленные сведения.
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
		{
			if (Equals(field, value))
				return false;
			field = value;
			OnPropertyChanged(PropertyName);
			return true;
		}
	}
}