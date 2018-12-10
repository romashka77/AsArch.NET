using AsArch.NET.Interfaces;
using System;
using System.Linq;
using System.Web.Http;
//using System.Web.UI;

namespace AsArch.NET.Controllers
{
    public class DictController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public DictController(IRepository repository)
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
        // GET: api/Dict
        [HttpGet]
        public IHttpActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/Dict/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var res = repository.ListDict().Where(d => d.ID_ATTR == id).OrderBy(n => n.STR_NAME).ToList().Select(n => n.STR_NAME);
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

        //// POST: api/Dict
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Dict/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Dict/5
        //public void Delete(int id)
        //{
        //}
    }
}
