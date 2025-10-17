using ProjectTest.Model;
using ProjectTest.Model.Context;
using ProjectTest.Repository.Generic;

namespace ProjectTest.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(SqlContext context) : base(context)
        {
        }
        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id)))
                return null;

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Ativo = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
