namespace ASP.NET_Core_Web_Api.Services
{
    public interface IRepository<T,TId>
    {
        IList<T> GetAll();

        T GetById(TId id);

        TId Create(T data);

        bool Update(T data);

        bool Delete(TId id);
    }
}
