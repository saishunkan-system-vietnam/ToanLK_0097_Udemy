using Microsoft.AspNetCore.Mvc;

using System.IO;

namespace UdemyNet8.api.controller
{
    public class DefaultController : Controller
    {

        #region MULTIPLE ACTION METHOD

        [Route("mulitpleAction")]
        [Route("mulitpleActionMethod")]
        [Route("/")]
        public string MultipleActionMethod()
        {
            return "MultipleActionMethod";
        }

        [Route("nonmulitpleActionMethod")]
        public string NonMultipleActionMethod()
        {
            return "NonMultipleActionMethod";
        }

        #endregion

        #region TAKEOUTS ABOUT CONTROLLER

        // need of of two or both of condition:
        // + class need to be contain "Controller" at the end
        // + class need to be mark attribute [Controller]

        #endregion

        #region CONTENT RESULT

        [Route("content-result")]
        public ContentResult GetContentResult()
        {
            return Content("<h1>Hello World</h1>","text/html");
        }

        #endregion

        #region JSON RESULT

        [Route("json-result")]
        public JsonResult GetJsonResult() {

            return new JsonResult(new
            {
                name = "Hello world",
                age = 12
            });
        }

        #endregion

        #region FILE RESULT


        [Route("file/virtual-file")]
        public FileResult GetVirtualFileResult()
        {
            // VirtualFileResult([relative file path], [content type])
            return new VirtualFileResult("/Image/dummy.jpg", "application/jpg");
        }

        [Route("file/physical-file")]
        public FileResult GetPhysicalFileResult()
        {
            return new PhysicalFileResult("C:\\Users\\ToanLK\\source\\repos\\UdemyNet8\\UdemyNet8\\wwwroot\\Image\\dummy.jpg", "application/jpg");
        }

        [Route("file/file-content")]
        public FileResult GetFileContentResult()
        {
            byte[] fileByte = System.IO.File.ReadAllBytes("..\\UdemyNet8\\wwwroot\\Image\\dummy.jpg");
            return new FileContentResult(fileByte, "application/jpg");
        }

        #endregion

        #region IActionResult

        public IActionResult GetActionResult()
        {
            if (1 == 1)
            {
                return Content("hello", "text/plain");
            }
            else if (1 == 2)
            {
                return Json("console.log('hello')", "application/js"); 
            }
            else if (1 == 3)
            {
                return new PhysicalFileResult("C:\\Users\\ToanLK\\source\\repos\\UdemyNet8\\UdemyNet8\\wwwroot\\Image\\dummy.jpg", "application/jpg");
            }
        }

        #endregion

        #region STATUS CODE

        [Route("status-code")]
        public IActionResult GetStatusCode()
        {
            // set status code directly
            if (1 == 1)
            {
                Response.StatusCode = StatusCodes.Status201Created;
                return Content("<div style='color:yellow'>Hello World</div>","text/html");
            }
            // set StatusCode using return
            else if (1 == 1)
            {
                return Created("", "text/html");
                // can use [new CreatedResult()] instead
            }
        }

        #endregion

        #region REDIRECT

        [Route("redirect/from/{id}")]
        public IActionResult GetRedirectFrom(string id)
        {
            //return new RedirectResult("to"); // need path
            //return new RedirectToActionResult("GetRedirectTo", "Default", new { }); // need controller name and function
            //return RedirectToAction("GetRedirectTo", "Default", new { id });
            //return RedirectToActionPermanent("GetRedirectTo", "Default",new { id }); // return statusCode:301MovedPermanently
            //return new LocalRedirectResult($"~/redirect/to/{id}"); // using local url
            return new RedirectResult($"~/redirect/to/{id}",true); // need to define [permanent] param
        }

        [Route("redirect/to/{id}")]
        public IActionResult GetRedirectTo(string id)
        {
            return Content($"Redirect successfully, ID: {id}");
        }

        #endregion
    }
}
