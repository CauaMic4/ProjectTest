using ProjectTest.Data.VO;
using ProjectTest.Model;

namespace ProjectTest.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);

        List<Person> FindByName(string firstName, string lastName);

    }
}
