using System;

namespace GenericToolKit.DependencyInjection
{
	internal interface IServiceCollector : IServiceRegister
	{
		bool TryGetValue(Type serviceType, out ServiceCallSite callSite);

		void Dispose();
	}
}