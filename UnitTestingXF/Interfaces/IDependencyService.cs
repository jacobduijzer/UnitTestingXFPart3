namespace UnitTestingXF.Interfaces
{
	public interface IDependencyService
	{
		T Get<T>() where T : class;

		void Register<T, TImpl>() where T : class where TImpl : class, T;

		void Register<T>(T impl) where T : class;

		void Register<T>() where T : class;
	}
}
