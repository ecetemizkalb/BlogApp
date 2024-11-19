
namespace BLL.Services.Bases
{
    public interface IService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        public IQueryable<TModel> Query();
        public Service Create(TEntity entity);
        public Service Update(TEntity entity);
        public Service Delete(int id);
    }


}
