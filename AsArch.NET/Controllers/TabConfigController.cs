using AsArch.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AsArch.NET.Controllers
{
    public class TabConfigController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public TabConfigController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion
        #region Деструктор
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        // GET: api/TabConfig
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var res = repository.ListTabConfig().OrderBy(n => n.STR_NAME).ToList().Select(n => n.STR_NAME);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //// GET: api/TabConfig/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/TabConfig
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/TabConfig/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/TabConfig/5
        //public void Delete(int id)
        //{
        //}
    }
}
