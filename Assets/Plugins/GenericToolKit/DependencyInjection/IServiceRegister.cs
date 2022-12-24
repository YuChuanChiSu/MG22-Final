using System;

namespace GenericToolKit.DependencyInjection
{
	public interface IServiceRegister
	{
		bool TryRegisterSingleton(Type serviceType, Type implementationType);

		bool TryRegisterTransient(Type serviceType, Type implementationType);

		bool TryRegisterSingleton(Type serviceType, Func<IServiceProvider, object> factory, bool isDisposable = false);

		bool TryRigisterTransient(Type serviceType, Func<IServiceProvider, object> factory, bool isDisposable = false);

		bool TryRegisterConstant(Type serviceType, object implementation, bool isDisposable = false);

		bool TryRemove(Type serviceType);
	}
}