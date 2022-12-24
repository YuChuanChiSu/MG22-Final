
namespace GenericToolKit.Common
{
    public abstract class SingletonThread<T> where T : class, new()
    {
        private static T? _instance = null;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
