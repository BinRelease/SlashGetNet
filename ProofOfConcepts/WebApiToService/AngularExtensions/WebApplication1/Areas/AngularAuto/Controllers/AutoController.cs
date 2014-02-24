using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Areas.AngularAuto.Controllers
{
    public class AutoController : ApiController
    {
        [HttpGet]
        public string GetHammerStein()
        {
            return "this is working";
        }
    }
}
