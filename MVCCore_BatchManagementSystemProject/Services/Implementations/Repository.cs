using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        BatchManagementDbContext _dbcontext;
        public Repository(BatchManagementDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(TEntity entity)
        {
           _dbcontext.Set<TEntity>().Add(entity);
            Save();

        }

        public void Delete(int Id)
        {

            TEntity entity = GetById(Id);
            _dbcontext.Set<TEntity>().Remove(entity);
            Save();

        }

        public List<TEntity> GetAll()
        {
            return _dbcontext.Set<TEntity>().ToList();

        }

        public TEntity GetById(int Id)
        {
            return _dbcontext.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity entity)
        {

            _dbcontext.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        private void Save()
        {
            _dbcontext.SaveChanges();
        }
    }
}
