using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace AsArch.NET.Controllers
{
    public class SudZasController : ApiController
    {
        private IRepository repository;

        #region Конструктор
        public SudZasController(IRepository repository)
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
        // GET: api/SudZas
        public IHttpActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/SudZas/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var res = repository.GetSudZas(id).AsEnumerable();
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

        // POST: api/SudZas
        public IHttpActionResult Post(int id, [FromBody]SudZas model/*[FromBody]string value*/)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Переданные данные не прошли проверку");
                if (model.Order == -1)
                {
                    var sud_zas = repository.GetSudZas(id);
                    if (sud_zas == null || sud_zas.Count() == 0) model.Order = 0;
                    else model.Order = sud_zas.Max(n => n.Order) + 1;
                    model.N = (model.Order + 1).ToString();
                }
                repository.PostSudZas(id, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //// PUT: api/SudZas/5
        //public void Put(int id, [FromBody]string value)
        //{

        //}

        // DELETE: api/SudZas/5
        public IHttpActionResult Delete(int id, BaseOrders model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Переданные данные не прошли проверку");

                string[] ids = model.Orders.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string n = string.Empty;
                foreach (var item in ids)
                {
                    var i = int.Parse(item);
                    repository.DeleteSudZas(id, i);
                    n = $"{n} {i + 1}";
                }
                return Ok($"Удалено {n}.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
