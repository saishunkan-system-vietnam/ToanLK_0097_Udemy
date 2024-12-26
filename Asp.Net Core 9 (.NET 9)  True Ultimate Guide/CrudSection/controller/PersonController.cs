using CrudSection.Model;
using CrudSection.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace CrudSection.controller
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService) {
            _personService = personService;
        }

        [Route("person/index")]
        [Route("/")]
        public IActionResult Index(string? searchBy, string? keyword, string sortby = (nameof(Person.Name)), int sortorder = 0)
        {
            ViewBag.SearchBy = searchBy;
            ViewBag.Keyword = keyword;
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {
                    nameof(Person.Id),
                    "Id"
                },
                {
                    nameof(Person.Name),
                    "Name"
                },
                {
                    nameof(Person.Job),
                    "Job"
                },
                {
                    nameof(Person.Age),
                    "Age"
                }
            };
            List<Person> list = _personService.GetFilter(searchBy,keyword);
            list = _personService.GetSort(list, sortby, sortorder);
            ViewBag.SortOrder = sortorder;
            ViewBag.SortBy = sortby;
            return View(list);
        }
    }
}
