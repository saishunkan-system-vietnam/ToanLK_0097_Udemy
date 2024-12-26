using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UdemyNet8.api.model;
using UdemyNet8.CustomModelBinder;

namespace UdemyNet8.api.controller
{
    [Controller]
    public class ModelBindingAndValidationController : Controller
    {
        #region QUERY STRING

        // ?name=LekhanhToan&age=12
        [Route("query-string")]
        public IActionResult GetQueryString(string name, int age)
        {
            return Content($"{name}-{age}", "text/plain");
        }

        [Route("route-value/{name}/{age}")]
        public IActionResult GetRouteValue(string name, int age)
        {
            return Content($"{name}-{age}", "text/plain");
        }

        [Route("from-route/{name}/{age}")]
        public IActionResult GetFromRoute([FromRoute] string name, [FromRoute] int age)
        {
            return Content($"{name}-{age}", "text/plain");
        }

        // ?name=LekhanhToan&age=12
        [Route("from-query")]
        public IActionResult GetFromQuery([FromQuery] string name, [FromQuery] int age)
        {
            return Content($"{name}-{age}", "text/plain");
        }

        [Route("model-class")]
        public IActionResult GetModelClass([FromQuery] Book book)
        {
            return Content(book.ToString(), "text/plain");
        }

        //when we use x-www-form-urlencodes or multipart/form-data, we don’t need to define[FromForm]
        //but when we send data with type “application/json”, we need to define[FromBody]
        [Route("form-url-encode")]
        public IActionResult GetFormUrlEncode(Book book)
        {
            return Content(book.ToString(), "text/plain");
        }

        [Route("form-data")]
        public IActionResult GetFormData(Book book)
        {
            return Content(book.ToString(), "text/plain");
        }

        [Route("model-validation")]
        public IActionResult GetModelValidation(ValidationModel validateThis)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = "";
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        errorMessage += $"-{error.ErrorMessage}";
                    }
                }
                if (errorMessage != "")
                {
                    return Content($"Error detected\n {errorMessage}", "text/plain");
                }
            }
            return Content($"If you see this text, that means the validation succeed\n{validateThis.ToString()}", "text/plain");
        }

        [Route("i-validatable-object")]
        public IActionResult GetIValidatableObject(ValidatableObject value)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = "";
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        errorMessage += $"-{error.ErrorMessage}";
                    }
                }
                if (errorMessage != "")
                {
                    return Content($"Error detected\n {errorMessage}", "text/plain");
                }
            }
            return Content($"If you see this text, that means the validation succeed\n{value.ToString()}", "text/plain");
        }

        [Route("bind")]
        public IActionResult GetBind([Bind(nameof(Book.Id),nameof(Book.Title), nameof(Book.NumberOfCharacter))] Book book)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = "";
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        errorMessage += $"-{error.ErrorMessage}";
                    }
                }
                if (errorMessage != "")
                {
                    return Content($"Error detected\n {errorMessage}", "text/plain");
                }
            }
            return Content($"If you see this text, that means the validation succeed\n{book.ToString()}", "text/plain");
        }

        [Route("from-body")]
        public IActionResult GetFromBody([FromBody]Book book)
        {
            return Content($"{book.ToString()}", "text/plain");
        }

        [Route("custom-binder")]
        public IActionResult GetCustomBinder([FromBody] Book book)
        {
            return Content($"{book.ToString()}", "text/plain");
        }

        [Route("collection-binder")]
        public IActionResult GetCollectionBinder([FromBody] ValidationModel validate)
        {
            return Content($"{validate.ToString()}", "text/plain");
        }

        [Route("from-header")]
        public IActionResult GetFromHeader([FromHeader(Name = "Method")] string method)
        {
            return Content($"{method}", "text/plain");
        }

        [Route("/remote-validate")]
        public IActionResult RemoteTestAciontResult([FromBody] Book book)
        {
            Console.WriteLine(book.Title);
            return Content("hello world");
        }

        public JsonResult IsUID_Available(string title)
        {

            if(title.Length < 12)
            {

            }
            return Json("vlue");
        }



        #endregion
    }
}
