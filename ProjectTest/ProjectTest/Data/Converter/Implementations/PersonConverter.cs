using ProjectTest.Data.Converter.Contract;
using ProjectTest.Data.VO;
using ProjectTest.Model;

namespace ProjectTest.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {

        public Person Parse(PersonVO origin)
        {
            if (origin == null)
                return null;

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                Ativo = origin.Ativo
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null)
                return null;

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                Ativo = origin.Ativo
            };
        }
        
        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(item => Parse(item)).Where(x=>x.Ativo == true).ToList();
        }


        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(item => Parse(item)).Where(x => x.Ativo == true).ToList();
        }
    }
}
