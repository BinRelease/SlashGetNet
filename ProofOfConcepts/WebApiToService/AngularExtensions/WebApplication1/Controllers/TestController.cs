using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestController : ApiController
    {
        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isfart">if set to <c>true</c> [isfart].</param>
        /// <returns>a fart model</returns>
        [HttpGet]
        public Fart Get(int id, bool isfart)
        {
            return new Fart()
            {
                ID = 123,
                IsFart = true,
                Name = "ohmygodfart"
            };
        }

        [HttpGet]
        public List<Fart> Get()
        {
            return new List<Fart>()
            {
                new Fart()
                {
                    ID = 123,
                    IsFart = true,
                    Name = "ohmygodfart"
                }
            };
        }

        /// <summary>
        /// Posts the specified randomnumber.
        /// </summary>
        /// <param name="randomnumber">The randomnumber.</param>
        /// <param name="fart">The fart.</param>
        [HttpPost]
        public void PostifyThisGuy(int id, [FromBody]Fart fart)
        {
        }

        [HttpPut]
        public void Save([FromBody]Fart fart)
        {

        }

        [HttpDelete]
        public void d(int id)
        {

        }
    }
}
