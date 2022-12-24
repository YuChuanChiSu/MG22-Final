
namespace GenericToolKit.Common
{
	public abstract class SingletonStatic<T> where T : class, new()
	{
		private static T _instance = new T();

		public static T Instance => _instance;
	}
}