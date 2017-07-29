using System.Threading.Tasks;

namespace UnitTestingXF.Interfaces
{
    public interface ICustomerApi 
    {
        Task<bool> LoginAsync(string username, string password);
    }
}

