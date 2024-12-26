using System.Numerics;
using Xunit;
using XUnitSection.Model;
using XUnitSection.Services.IService;
using XUnitSection.Services.Service;

namespace XUnitSection.XUnitTest
{
    public class BookServiceTest
    {
        private readonly IBookService _bookService;
        public BookServiceTest()
        {
            _bookService = new BookService();
        }

        [Fact]
        public async Task AddBook_NullBook()
        {
            //Arrange
            Book addBook = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _bookService.AddBook(addBook);
            });
        }

        [Fact]
        public async Task AddBook_TitleIsNull()
        {
            Book addBook = new Book()
            {
                Id = 1,
                Title = null,
                Author = "Me"
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _bookService.AddBook(addBook);
            });
        }

        [Fact]
        public async Task AddBook_DuplicateTitlle()
        {
            // Arrange
            Book addBook = new Book()
            {
                Id = 1,
                Title = "Hello",
                Author = "Me"
            };

            Book addBook2 = new Book()
            {
                Id = 2,
                Title = "Hello",
                Author = "Me"
            };

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _bookService.AddBook(addBook);
                _bookService.AddBook(addBook2);
            });
        }

        [Fact]
        public async Task AddBook_WithShortTitleLength()
        {
            // Arrange
            Book addBook = new Book()
            {
                Id = 1,
                Title = "me",
                Author = "ToanLK"
            };

            // Assert
            // Act
            Assert.True(_bookService.AddBook(addBook));
        }

        [Fact]
        public async Task AddBook_ProperValue()
        {
            // Arrange
            Book addBook = new Book()
            {
                Id = 1,
                Title = "Hello",
                Author = "Me"
            };

            // Assert 
            // Act
            Assert.True(_bookService.AddBook(addBook));
        }

        [Fact]
        public void GetBook_EmptyList()
        {
            // Act
            List<Book> actual_books = _bookService.GetBooks();

            //Assert
            Assert.Empty(actual_books);
        }

        [Fact]
        public void GetBook_AFewBooks()
        {
            // Arrange
            Book book = new Book(1, "Hello", "Me");
            Book book2 = new Book(2, "Ehehe", "Me");
            Book book3 = new Book(3, "Ehuhu", "Me");
            Book book4 = new Book(4, "Ehihi", "Me");
            _bookService.AddBook(book);
            _bookService.AddBook(book2);
            _bookService.AddBook(book3);
            _bookService.AddBook(book4);

            // Act
            List<Book> actual_book = _bookService.GetBooks();
            //Assert

            foreach(Book item in actual_book)
            {
                Assert.Contains(item, actual_book);
            }
        }

        [Fact]
        public void GetById_EmptyId()
        {
            // Arrange
            int? Id = null;

            // Act
            Book foundBook = _bookService.GetById(Id);

            // Assert
            Assert.Null(foundBook);
        }

        [Fact]
        public void GetById_validId()
        {
            // Arrange
            Book addBook = new Book(1, "Hello", "Me");
            _bookService.AddBook(addBook);
            // Act
            Book? foundBook = _bookService.GetById(addBook.Id);
            //Assert
            Assert.Equal(addBook, foundBook);
        }







    }
}
