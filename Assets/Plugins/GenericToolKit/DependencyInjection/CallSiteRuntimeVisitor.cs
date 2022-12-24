using System;

namespace GenericToolKit.DependencyInjection
{
	internal class CallSiteRuntimeVisitor
	{
		public object VisitCallSite(ServiceCallSite callSite, IServiceProvider serviceProvider)
		{
			return callSite.CallSiteKind switch
			{
				CallSiteKind.Constructor => VisitConstructor((ConstructorCallSite)callSite, serviceProvider),
				CallSiteKind.Constant => VisitConstant((ConstantCallSite)callSite),
				CallSiteKind.Factory => VisitFactory((FactoryCallSite)callSite, serviceProvider),
				_ => throw new NotImplementedException(),
			};
		}

		private object VisitConstructor(ConstructorCallSite callSite, IServiceProvider serviceProvider)
		{
			if (callSite.ServiceLifeCircle == ServiceLifeCircle.Singleton)
			{
				if (callSite.Implementation == null)
				{
					object[] constructorParams2 = new object[callSite.ConstructorParams.Length];
					for (uint j = 0u; j < callSite.ConstructorParams.Length; j++)
					{
						constructorParams2[j] = VisitCallSite(callSite.ConstructorParams[j], serviceProvider);
					}
					callSite.Implementation = callSite.ConstructorInfo.Invoke(constructorParams2);
				}
				return callSite.Implementation;
			}
			object[] constructorParams = new object[callSite.ConstructorParams.Length];
			for (uint i = 0u; i < callSite.ConstructorParams.Length; i++)
			{
				constructorParams[i] = VisitCallSite(callSite.ConstructorParams[i], serviceProvider);
			}
			return callSite.ConstructorInfo.Invoke(constructorParams);
		}

		private object VisitFactory(FactoryCallSite callSite, IServiceProvider serviceProvider)
		{
			if (callSite.ServiceLifeCircle == ServiceLifeCircle.Singleton)
			{
				if (callSite.Implementation == null)
				{
					callSite.Implementation = callSite.Factory(serviceProvider);
				}
				return callSite.Implementation;
			}
			return callSite.Factory(serviceProvider);
		}

		private object VisitConstant(ConstantCallSite callSite)
		{
			return callSite.Implementation!;
		}
	}
}