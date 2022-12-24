using System;
using System.Threading;
using GenericToolKit.Common;

namespace GenericToolKit.DependencyInjection
{
	public sealed class IocContainer : SingletonStatic<IocContainer>, IServiceProvider, IDisposable
	{
		private volatile IServiceProvider? _serviceProvider;

		public void ConfigureServices(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			if (Interlocked.CompareExchange(ref _serviceProvider, serviceProvider, null) != null)
			{
				throw new InvalidOperationException("The ServiceProvider has already been configured.");
			}
		}

		public static IServiceRegister BuildDefalutRegister()
		{
			return new ServiceCollector();
		}

		public static void ConfigureDefalutService(IServiceRegister collector)
		{
			SingletonStatic<IocContainer>.Instance.ConfigureServices(new ServiceProvider((IServiceCollector)collector));
		}

		public object? GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			return (_serviceProvider ?? throw new InvalidOperationException("The ServiceProvider has not been configured.")).GetService(serviceType);
		}

		public T? GetService<T>() where T : class
		{
			return (_serviceProvider ?? throw new InvalidOperationException("The ServiceProvider has not been configured.")).GetService(typeof(T)) as T;
		}

		public T GetRequiredService<T>() where T : class
		{
			return ((_serviceProvider ?? throw new InvalidOperationException("The ServiceProvider has not been configured.")).GetService(typeof(T)) as T) ?? throw new InvalidOperationException("The service was not configured.");
		}

		public bool TryDisposeSingleton<T>()
		{
			return (_serviceProvider as IServiceRegister)?.TryRemove<T>() ?? false;
		}

		public void Dispose()
		{
			(_serviceProvider as IDisposable)?.Dispose();
		}
	}
}