using ProjectTest.Model;
using ProjectTest.Model.Base;

namespace ProjectTest.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);   
    }
}
