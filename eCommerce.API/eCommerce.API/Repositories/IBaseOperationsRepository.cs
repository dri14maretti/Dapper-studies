using eCommerce.API.Models;

namespace eCommerce.API.Repositories
{
    public interface IBaseOperationsRepository<T> where T : class
    {
        public List<T> Get();
        public T GetById(int id);
        public void Insert(T objeto);
        public void Delete(int id);
        public void Update(T objeto);

    }
}
