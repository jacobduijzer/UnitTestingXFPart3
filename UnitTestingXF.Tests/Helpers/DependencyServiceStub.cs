using System;
using System.Collections.Generic;
using UnitTestingXF.Interfaces;

namespace UnitTestingXF.Tests.Helpers
{
	public class DependencyServiceStub : IDependencyService
	{
		private readonly Dictionary<Type, object> registeredServices = new Dictionary<Type, object>();

		public T Get<T>() where T : class
		{
			try
			{
				return (T)registeredServices[typeof(T)];
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Service not registered: {nameof(T)}");
				return null;
			}
		}

		public void Register<T>() where T : class
		{
			throw new NotImplementedException();
		}

		public void Register<T, TImpl>() where T : class where TImpl : class, T
		{
			this.registeredServices[typeof(T)] = default(TImpl);
		}

		public void Register<T>(T impl) where T : class
		{
			this.registeredServices[typeof(T)] = impl;
		}
	}
}
