using System;
using System.Reflection;

namespace GenericToolKit.DependencyInjection
{
	internal class ConstructorCallSite : ServiceCallSite
	{
		internal ConstructorInfo ConstructorInfo { get; }

		internal ServiceCallSite[] ConstructorParams { get; }

		public override Type ServiceType { get; }

		public override Type ImplementationType => ConstructorInfo.DeclaringType;

		public override CallSiteKind CallSiteKind => CallSiteKind.Constructor;

		public override ServiceLifeCircle ServiceLifeCircle { get; }

		public override Func<IServiceProvider, object>? Factory => null;

		public ConstructorCallSite(Type serviceType, ConstructorInfo constructorInfo, ServiceLifeCircle lifeCircle, ServiceCallSite[] constructorParams)
		{
			if (!serviceType.IsAssignableFrom(constructorInfo.DeclaringType))
			{
				throw new ArgumentException(constructorInfo.DeclaringType.Name + " cannot be convered to" + serviceType.Name);
			}
			ServiceType = serviceType;
			ConstructorInfo = constructorInfo;
			ServiceLifeCircle = lifeCircle;
			ConstructorParams = constructorParams;
		}
	}
}