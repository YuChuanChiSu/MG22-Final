using System;

namespace GenericToolKit.DependencyInjection
{
	internal class FactoryCallSite : ServiceCallSite
	{
		public override Type ServiceType { get; }

		public override Type? ImplementationType => null;

		public override CallSiteKind CallSiteKind => CallSiteKind.Factory;

		public override ServiceLifeCircle ServiceLifeCircle { get; }

		public override Func<IServiceProvider, object> Factory { get; }

		public override bool IsDisposable { get; }

		public FactoryCallSite(Type serviceType, ServiceLifeCircle lifeCircle, Func<IServiceProvider, object> factory, bool isDisposable)
		{
			ServiceType = serviceType;
			ServiceLifeCircle = lifeCircle;
			Factory = factory;
			IsDisposable = isDisposable;
		}
	}
}