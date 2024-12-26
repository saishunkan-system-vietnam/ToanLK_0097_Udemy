using XUnitSection.Model;
using XUnitSection.Services.Service;

namespace XUnitSection.Services.IService
{
    public class BookService : IBookService
    {
        List<Book> bookRepo = new List<Book>();

        public bool AddBook(Book book) 
        {
            //if(book == null)
            //{
            //    throw new ArgumentNullException("Book", "Can not be null");
            //}
            var isDuplicate = false;

            if(book.Title == null)
            {
                throw new ArgumentException("Title", "Can not be null");
            }
            foreach (var item in bookRepo)
            {
                if (item.Title == book.Title)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (isDuplicate)
            {
                throw new ArgumentException("Title", "Can not be duplicate");
            }
            string bookPrefix = book.Title.Substring(0, 4);
            
            bookRepo.Add(book);
            return true;
        }

        public List<Book> GetBooks()
        {
            return bookRepo;
        }

        public Book? GetById(int? id)
        {
            if(id == null)
            {
                return null;
            }

            var foundBook = bookRepo.FirstOrDefault((book) =>
            {
                return book.Id == id;
            });

            return foundBook;
        }
    }
}
