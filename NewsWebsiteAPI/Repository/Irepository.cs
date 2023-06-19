namespace NewsAPI.Repository
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T? GetDetails(int id);
        public void Insert(T t);
        public void Update(T entity);
        public T Delete(int id);
    }
}
