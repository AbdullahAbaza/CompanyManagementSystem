using Company.DAL.Models;
using System.Linq;

namespace Company.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IQueryable<Department> SearchByName(string name);
    }
}
