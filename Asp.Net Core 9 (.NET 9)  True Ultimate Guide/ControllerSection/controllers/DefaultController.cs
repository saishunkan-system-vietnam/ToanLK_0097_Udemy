using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerSection.controllers
{
    public class DefaultController 
    {
        [Route("get")]
        public string Get()
        {
            return "Hello World";
        }
    }
}
