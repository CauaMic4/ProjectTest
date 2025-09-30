using ProjectTest.Model;
using ProjectTest.Model.Context;

namespace ProjectTest.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private SqlContext _context;
        public BookRepositoryImplementation(SqlContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Book.Where(p=>p.Ativo == true).ToList();
        }

        public Book FindById(long id)
        {
            return _context.Book.Where(p => p.Ativo == true).SingleOrDefault(p => p.Id.Equals(id)) ;
        }

        public Book Create(Book Book)
        {
            try
            {
                Book.Ativo = true;

                _context.Add(Book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return Book;
        }

        public Book Update(Book Book)
        {
            if (!Exists(Book.Id)) return null;

            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(Book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(Book);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return Book;
        }

        public void Delete(long id)
        {
            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(id));

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
            return _context.Book.Any(p => p.Id.Equals(id));
        }
    }
}
