using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace GenericToolKit.DependencyInjection
{
	internal class ServiceCollector : IDisposable, IServiceCollector, IServiceRegister
	{
		private ConcurrentDictionary<Type, ServiceCallSite> _cache = new ConcurrentDictionary<Type, ServiceCallSite>();

		public bool TryRegisterSingleton(Type serviceType, Type implementationType)
		{
			return TryRegisterConstructor(serviceType, implementationType, ServiceLifeCircle.Singleton);
		}

		public bool TryRegisterTransient(Type serviceType, Type implementationType)
		{
			return TryRegisterConstructor(serviceType, implementationType, ServiceLifeCircle.Transient);
		}

		public bool TryRegisterSingleton(Type serviceType, Func<IServiceProvider, object> factory, bool isDisposable = false)
		{
			return TryRegisterFatory(serviceType, factory, ServiceLifeCircle.Singleton, isDisposable);
		}

		public bool TryRigisterTransient(Type serviceType, Func<IServiceProvider, object> factory, bool isDisposable = false)
		{
			return TryRegisterFatory(serviceType, factory, ServiceLifeCircle.Transient, isDisposable);
		}

		public bool TryRegisterConstant(Type serviceType, object implementation, bool isDisposable = false)
		{
			if (_cache.ContainsKey(serviceType))
			{
				return false;
			}
			_cache[serviceType] = new ConstantCallSite(serviceType, implementation, isDisposable);
			return true;
		}

		private bool TryRegisterConstructor(Type serviceType, Type implementationType, ServiceLifeCircle lifeCircle)
		{
			if (_cache.ContainsKey(serviceType))
			{
				return false;
			}
			ConstructorInfo constructor = GetConstructor(implementationType);
			ParameterInfo[] parameters = constructor.GetParameters();
			ServiceCallSite[] callSites = new ServiceCallSite[parameters.Length];
			for (uint i = 0u; i < parameters.Length; i++)
			{
				callSites[i] = _cache[parameters[i].ParameterType];
			}
			_cache[serviceType] = new ConstructorCallSite(serviceType, constructor, lifeCircle, callSites);
			return true;
		}

		private bool TryRegisterFatory(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifeCircle lifeCircle, bool isDisposable = false)
		{
			if (_cache.ContainsKey(serviceType))
			{
				return false;
			}
			_cache[serviceType] = new FactoryCallSite(serviceType, lifeCircle, factory, isDisposable);
			return true;
		}

		private ConstructorInfo GetConstructor(Type implementationType)
		{
			ConstructorInfo[] constructors = implementationType.GetConstructors();
			if (constructors.Length > 2)
			{
				return GetPreferenceConstructor(constructors);
			}
			if (constructors.Length == 1)
			{
				return constructors[0];
			}
			if (constructors.Any((ConstructorInfo constructor) => constructor.Name == ".cctor"))
			{
				return constructors.Where((ConstructorInfo constructor) => constructor.Name != ".cctor").FirstOrDefault();
			}
			return GetPreferenceConstructor(constructors);
		}

		private ConstructorInfo GetPreferenceConstructor(ConstructorInfo[] constructors)
		{
			ConstructorInfo preference = constructors.Where((ConstructorInfo constructor) => constructor.GetCustomAttributes<PreferenceConstructorAttribute>().Any()).FirstOrDefault();
			if (preference != null)
			{
				return preference;
			}
			throw new ArgumentException("Cannot find preference constructor.");
		}

		public bool TryRemove(Type serviceType)
		{
			if (_cache.TryRemove(serviceType, out var callSite))
			{
				if (callSite.IsDisposable && callSite.ServiceLifeCircle == ServiceLifeCircle.Singleton)
				{
					(callSite.Implementation as IDisposable)?.Dispose();
				}
				return true;
			}
			return false;
		}

		public bool TryGetValue(Type serviceType, out ServiceCallSite callSite)
		{
			return _cache.TryGetValue(serviceType, out callSite);
		}

		public void Dispose()
		{
			foreach (ServiceCallSite callSite in _cache.Values)
			{
				if (callSite.IsDisposable && callSite.ServiceLifeCircle == ServiceLifeCircle.Singleton)
				{
					(callSite.Implementation as IDisposable)?.Dispose();
				}
			}
		}
	}
}