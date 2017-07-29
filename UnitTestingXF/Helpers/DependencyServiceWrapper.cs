using System;
using UnitTestingXF.Interfaces;
using Xamarin.Forms;

namespace UnitTestingXF.Helpers
{
	public class DependencyServiceWrapper : IDependencyService
	{
		static readonly Lazy<DependencyServiceWrapper> instance = new Lazy<DependencyServiceWrapper>(() => new DependencyServiceWrapper());

		public static DependencyServiceWrapper Instance
		{
			get { return instance.Value; }
		}

		public T Get<T>() where T : class
		{
			// The wrapper will simply pass everything through to the real Xamarin.Forms DependencyService class when not unit testing
			return DependencyService.Get<T>();
		}

		public void Register<T>() where T : class
		{
			DependencyService.Register<T>();
		}

		public void Register<T, TImpl>() where T : class where TImpl : class, T
		{
			DependencyService.Register<T, TImpl>();
		}

		public void Register<T>(T impl) where T : class
		{
			throw new NotSupportedException();
		}
	}
}
