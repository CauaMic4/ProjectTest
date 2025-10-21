using ProjectTest.Data.Converter.Implementations;
using ProjectTest.Data.VO;
using ProjectTest.Hypermedia.Utils;
using ProjectTest.Model;
using ProjectTest.Repository;

namespace ProjectTest.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagesSearchVO<PersonVO> FindWithPagesSearch(string name, string sortDirection, int pageSize, int page)
        {
            var offset = page > 0 ? (page - 1) * pageSize : 0;
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc", StringComparison.InvariantCultureIgnoreCase) ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;

            string query = @"select * from Persons p where 1 = 1 ";
            string countQuery = @"select count(*) from Persons p where 1 = 1 ";

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagesSearchVO<PersonVO>
            {
                CurrentPage = page,
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults,
                List = _converter.Parse(persons)
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);

            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            var user = _converter.Parse(_repository.FindById(id));

            user.Ativo = false;

            Update(user);
        }
    }
}
