using Company.DAL.Models;
using System.Linq;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByAddress(string address);

        IQueryable<Employee> SearchByName(string name);
    }


}
