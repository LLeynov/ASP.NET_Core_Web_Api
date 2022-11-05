namespace ASP.NET_Core_Web_Api.Services
{
    public interface IRepository<T,TId>
    {
        IList<T> GetAll();

        T GetById(TId id);

        int Create(T data);

        void Update(T data);

        void Delete(T id);
    }
}
