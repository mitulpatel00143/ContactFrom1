using ContactFrom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactFrom.Controllers
{
    public class ContactApiController : ApiController
    {
        [Route("api/ContactApi/Index")]
        [HttpPost]
        public IHttpActionResult Index(Contact obj)
        {
            var result = new Contact().InsertContact(obj);
            return Ok(new { Success = result });
        }
    }
}
