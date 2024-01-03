using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data;

namespace Company.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            DepartmentRepository = new DepartmentRepository(dbContext);
            EmployeeRepository = new EmployeeRepository(dbContext);
        }


        public int Complete() => _dbContext.SaveChanges();

        public void Dispose() => _dbContext.Dispose();

    }
}
