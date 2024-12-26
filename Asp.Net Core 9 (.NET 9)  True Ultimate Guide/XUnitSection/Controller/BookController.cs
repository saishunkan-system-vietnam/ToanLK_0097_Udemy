using Microsoft.AspNetCore.Mvc;
using XUnitSection.Model;
using XUnitSection.Services.Service;

namespace XUnitSection.Controller
{
    [ApiController]
    public class BookController
    {
        private IBookService _bookService;
        public BookController(IBookService bookservice)
        {
            _bookService = bookservice;
        }

        [HttpPost("/add")]
        public IActionResult AddBook([FromBody]Book book)
        {
            bool result = _bookService.AddBook(book);
            return new ContentResult()
            {
                Content = result.ToString(),
                ContentType = "text/plain"
            };
        }

        public IActionResult GetBooks()
        {
            return new JsonResult(_bookService.GetBooks());
        }
    }
}
