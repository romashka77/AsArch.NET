using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;

namespace AsArch.NET.Controllers
{
    public class StoronaProcParamController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public StoronaProcParamController(IRepository repository)
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
        
        // GET: api/StoronaProcParam/5
        public StoronaProcParam Get(int id)
        {
            return repository.StoronaProcParam(id);
        }

        // POST: api/StoronaProcParam
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/StoronaProcParam/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/StoronaProcParam/5
        //public void Delete(int id)
        //{
        //}
    }
}
