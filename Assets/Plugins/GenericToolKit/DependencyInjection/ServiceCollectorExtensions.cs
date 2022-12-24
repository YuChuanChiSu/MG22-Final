using System;

namespace GenericToolKit.DependencyInjection
{
	public static class ServiceCollectorExtensions
	{
		public static bool TryRegisterSingleton<TService, TImplementation>(this IServiceRegister collector)
		{
			return collector.TryRegisterSingleton(typeof(TService), typeof(TImplementation));
		}

		public static bool TryRegisterTransient<TService, TImplementation>(this IServiceRegister collector)
		{
			return collector.TryRegisterTransient(typeof(TService), typeof(TImplementation));
		}

		public static bool TryRegisterSingleton<TService>(this IServiceRegister collector, Func<IServiceProvider, object> factory, bool isDisposable = false)
		{
			return collector.TryRegisterSingleton(typeof(TService), factory, isDisposable);
		}

		public static bool TryRigisterTransient<TService>(this IServiceRegister collector, Func<IServiceProvider, object> factory, bool isDisposable = false)
		{
			return collector.TryRigisterTransient(typeof(TService), factory, isDisposable);
		}

		public static bool TryRegisterConstant<TService>(this IServiceRegister collector, object implementation, bool isDisposable = false)
		{
			return collector.TryRegisterConstant(typeof(TService), implementation, isDisposable);
		}

		public static bool TryRemove<TService>(this IServiceRegister collector)
		{
			return collector.TryRemove(typeof(TService));
		}
	}
}