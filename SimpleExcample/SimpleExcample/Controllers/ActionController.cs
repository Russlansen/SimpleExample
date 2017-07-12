using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleExcample.Controllers
{
    public class ActionController : ApiController
    {
        public string Get()
        {
            return "Test data from GET method";
        }

        public string Post(string text)
        {
            return text;
        }
    }
}
