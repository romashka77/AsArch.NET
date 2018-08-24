using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AsArch.NET.Controllers
{
    public class StoronaProcController : ApiController
    {
        // GET: api/StoronaProc
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StoronaProc/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/StoronaProc
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StoronaProc/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StoronaProc/5
        public void Delete(int id)
        {
        }
    }
}
