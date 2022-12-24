using System;

namespace GenericToolKit.DependencyInjection
{
	internal abstract class ServiceCallSite
	{
		public abstract Type ServiceType { get; }

		public abstract Type? ImplementationType { get; }

		public abstract CallSiteKind CallSiteKind { get; }

		public abstract ServiceLifeCircle ServiceLifeCircle { get; }

		public object? Implementation { get; set; }

		public abstract Func<IServiceProvider, object>? Factory { get; }

		public virtual bool IsDisposable
		{
			get
			{
				if (ImplementationType != null)
				{
					return typeof(IDisposable).IsAssignableFrom(ImplementationType);
				}
				return false;
			}
		}
	}
}