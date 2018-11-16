using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AsArch.NET.Controllers
{
    public class DocIskController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public DocIskController(IRepository repository)
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

        public IQueryable<DocIsk> Get(int id)
        {
            return null;//repository.ListStoronaProc(id);
        }

    }
}
