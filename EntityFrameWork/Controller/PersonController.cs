using EntityFrameWork.Entities;
using EntityFrameWork.Filters;
using EntityFrameWork.Filters.ActionFilters;
using EntityFrameWork.Filters.AuthorizationFilter;
using EntityFrameWork.Filters.ExceptionFilters;
using EntityFrameWork.Filters.ResultFilters;
using EntityFrameWork.Service;
using Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;

namespace EntityFrameWork.Controller
{
    [Route("person")]
    [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "keyFromController", "Value from controller", 1 })]
    [TypeFilter(typeof(PersonAlwaysRunResultFilter))]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;

        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService personService, ILogger<PersonController> logger, ICountryService countryService)
        {

            _personService = personService;
            _logger = logger;
            _countryService = countryService;
        }

        [Route("/")]
        [TypeFilter(typeof(PersonIndexActionfilter),Order = 4)]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "customkey", "dummy value", 3 })]
        [TypeFilter(typeof(GetPeopleResultFilter))]
        [TypeFilter(typeof(TokenAuthorizationFilter))]
        [TypeFilter(typeof(HandleExceptionFilter))]
        [SkipFilter]
        public IActionResult GetAllPerson(string? guid)
        {
            Console.WriteLine(guid);
            _logger.LogInformation("get all person api of PersonController");
            List<Person> list = _personService.GetAllPerson();
            list.ForEach((person) => { person.Country.Person = null; });
            _logger.LogDebug($"country of first person in list: {list[0]}");
            return new JsonResult(list);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            if(guid == Guid.Empty)
            {
                _logger.LogError("guid is null");
            }
            var returnVal = _personService.GetById(guid);
            
            return new JsonResult(returnVal);
        }

        [HttpGet("country")]
        public IActionResult GetPersonByContry()
        {
            _logger.LogWarning("Get All Country instead of Person");
            var result = _countryService.GetAllCountries();
            return new JsonResult(result);
        }


        [HttpPost("insert")]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "InsertKey", "Dummy Insert Person key", 2})]
        public IActionResult InsertPerson([FromBody] Person person)
        {
            _personService.AddPerson(person);
            return Ok();
        }

        [HttpPost]
        public JsonResult ValidationAddress(string address)
        {
            if (address.Length <= 100)
            {
                return new JsonResult(false);
            }
            else
            {
                return new JsonResult(true);
            }
        }

        [Route("error")]
        public IActionResult GetError()
        {
            string errorMessage = "";
            IExceptionHandlerFeature? exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(exception != null && exception.Error != null)
            {
                errorMessage = exception.Error.Message;
            }
            return new JsonResult(errorMessage);
        }

    }
}
