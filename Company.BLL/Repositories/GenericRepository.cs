using Company.BLL.Interfaces;
using Company.DAL.Data;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {

        private protected readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            //_dbContext.Set<T>().Add(entity);
            _dbContext.Add(entity); // Ef Core 3.1
        }
        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public T Get(int id)
        {
            /// Search in local Dbset first
            ///var T =  _dbContext.T.Local.Where(D => D.Id == id).FirstOrDefault();
            ///if (T is null)
            ///    T = _dbContext.T.Where(D => D.Id == id).FirstOrDefault();
            ///return T;
            ///return _dbContext.T.Find(id);

            return _dbContext.Find<T>(id); // EF Core 3.1 Feature

        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {

                return (IEnumerable<T>)_dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToList();

            }
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();

            // ToList() -> to work as immediate execution and we don't need tracking as we will display only
        }




    }
}
