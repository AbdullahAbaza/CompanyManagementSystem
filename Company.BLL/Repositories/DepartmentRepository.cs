using Company.BLL.Interfaces;
using Company.DAL.Data;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Department> SearchByName(string name)
        {
            return _dbContext.Set<Department>().AsNoTracking().Where(D => D.Name.ToLower().Contains(name));
        }
    }
}
