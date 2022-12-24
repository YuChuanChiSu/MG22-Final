using System;

namespace GenericToolKit.DependencyInjection
{
	internal class ConstantCallSite : ServiceCallSite
	{
		public override Type ServiceType { get; }

		public override Type ImplementationType { get; }

		public override CallSiteKind CallSiteKind => CallSiteKind.Constant;

		public override ServiceLifeCircle ServiceLifeCircle => ServiceLifeCircle.Singleton;

		public override Func<IServiceProvider, object>? Factory => null;

		public override bool IsDisposable { get; }

		public ConstantCallSite(Type serviceType, object implementation, bool isDisposable)
		{
			if (!serviceType.IsInstanceOfType(implementation))
			{
				throw new InvalidOperationException(serviceType.Name + " cannot be converted to " + implementation.GetType().Name);
			}
			ImplementationType = implementation.GetType();
			base.Implementation = implementation;
			ServiceType = serviceType;
			IsDisposable = isDisposable;
		}
	}
}