using System;

namespace GenericToolKit.DependencyInjection
{
	internal class ServiceProvider : IServiceProvider
	{
		private IServiceCollector _collector;

		private CallSiteRuntimeVisitor _callSiteRuntimeVisitor = new CallSiteRuntimeVisitor();

		public ServiceProvider(IServiceCollector collector)
		{
			_collector = collector;
		}

		public object? GetService(Type serviceType)
		{
			if (_collector.TryGetValue(serviceType, out var callSite))
			{
				return _callSiteRuntimeVisitor.VisitCallSite(callSite, this);
			}
			return null;
		}
	}
}