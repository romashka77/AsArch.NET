﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;

namespace AsArch.NET.Controllers
{
    public class StoronaProcController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public StoronaProcController(IRepository repository)
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

        // GET: api/StoronaProc
        //public string Get()
        //{
        //    return "Get()";
        //}
        // GET: api/StoronaProc/10654
        public IQueryable<StoronaProc> Get(int id)
        {
            return repository.ListStoronaProc(id);
        }


        // POST: api/StoronaProc
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/StoronaProc/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/StoronaProc/5
        //public void Delete(int id)
        //{
        //}
    }
}
