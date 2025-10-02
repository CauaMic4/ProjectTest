using ProjectTest.Data.Converter.Implementations;
using ProjectTest.Data.VO;
using ProjectTest.Model;
using ProjectTest.Repository;

namespace ProjectTest.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            try
            {
                var personEntity = _converter.Parse(person);
                personEntity = _repository.Create(personEntity);

                return _converter.Parse(personEntity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);

            return _converter.Parse(personEntity); 
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
