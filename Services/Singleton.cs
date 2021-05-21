namespace AutoParking.Services
{
	/// <summary>
	/// Гарантирует существование только одного объекта определённого класса, а также позволяет достучаться до этого объекта из любого места программы.
	/// </summary>
	/// <typeparam name="T">Тип класса-наследника</typeparam>
	abstract class Singleton<T> where T : new()
	{
		private static T _instance;

		public static T GetInstance()
		{
			if (_instance == null)
				_instance = new T();
			return _instance;
		}
	}
}

