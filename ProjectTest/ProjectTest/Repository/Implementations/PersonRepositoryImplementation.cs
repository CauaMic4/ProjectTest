using ProjectTest.Model;
using ProjectTest.Model.Context;

namespace ProjectTest.Repository.Implementations
{
    public class PersonRepositoryImplementation : IRepository<Person>
    {
        private SqlContext _context;
        public PersonRepositoryImplementation(SqlContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.Where(p=>p.Ativo == true).ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.Where(p => p.Ativo == true).SingleOrDefault(p => p.Id.Equals(id)) ;
        }

        public Person Create(Person person)
        {
            try
            {
                person.Ativo = true;

                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    result.Ativo = false;

                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
