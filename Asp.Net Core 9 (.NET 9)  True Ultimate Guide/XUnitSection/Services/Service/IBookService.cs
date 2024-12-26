using XUnitSection.Model;

namespace XUnitSection.Services.Service
{
    public interface IBookService
    {
        public bool AddBook(Book book);
        public List<Book> GetBooks();
        public Book? GetById(int? id);

    }
}
