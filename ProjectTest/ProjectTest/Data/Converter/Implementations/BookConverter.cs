using ProjectTest.Data.Converter.Contract;
using ProjectTest.Data.VO;
using ProjectTest.Model;

namespace ProjectTest.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null)
                return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Launch_date = origin.Launch_date,
                Price = origin.Price
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null)
                return null;

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Launch_date = origin.Launch_date,
                Price = origin.Price
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
