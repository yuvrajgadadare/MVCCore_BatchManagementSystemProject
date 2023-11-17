namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int Id);
        List<TEntity> GetAll();
        TEntity GetById(int Id);

    }
}
