using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsArch.NET.EntityDataModel;
using AsArch.NET.Interfaces;
using X.PagedList;
using AsArch.NET.Models;
using AsArch.NET.EntityDataModel.Entytis;

namespace AsArch.NET.Controllers
{
    //[Authorize]
    public class NodesController : Controller
    {
        //private DB_pfr_sap db = new DB_pfr_sap();
        private IRepository repository;
        #region Конструктор
        public NodesController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion
        #region Деструктор
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //repository.Dispose();
                repository = null;
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Index
        // GET: NODEs
        public async Task<ActionResult> Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            var nodes = repository.ListNode(id_parent: id).Select(n => new ItemNode
            {
                Icon = n.ITEMTYPE.ICON,
                Id = n.ID_NODE,
                Name = n.STR_LABEL,
                TypeName = n.ITEMTYPE.STR_NAME,
                Id_parent = n.ID_PARENT
            }); ;
            #region фильтрация
            if (searchString != null) { page = 1; }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            { nodes = nodes.Where(s => s.Name/* STR_LABEL*/.ToUpper().Contains(searchString.ToUpper())); }
            #endregion
            #region сортировка изменить на использование Linq.dynamic
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User desc" : "User";
            ViewBag.DateCreateSortParm = sortOrder == "DateCreate" ? "DateCreate desc" : "DateCreate";
            ViewBag.DateModify = sortOrder == "DateModify" ? "DateModify desc" : "DateModify";

            nodes = nodes.OrderBy(s => s.Name/* STR_LABEL*/);

            //switch (sortOrder)
            //{
            //    case "Name desc":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenByDescending(s => s.STR_LABEL);
            //        break;
            //    case "User":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenBy(s => s.USER.STR_LOGIN);
            //        break;
            //    case "User desc":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenByDescending(s => s.USER.STR_LOGIN);
            //        break;
            //    case "DateCreate":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenBy(s => s.DATE_CREATE);
            //        break;
            //    case "DateCreate desc":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenByDescending(s => s.DATE_CREATE);
            //        break;
            //    case "DateModify":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenBy(s => s.DATE_MODIFY);
            //        break;
            //    case "DateModify desc":
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenByDescending(s => s.DATE_MODIFY);
            //        break;
            //    default:
            //        nodes = nodes.OrderBy(s => s.ITEMTYPE.N_ORDER).ThenBy(s => s.STR_LABEL);
            //        break;
            //}
            #endregion
            #region пагинация
            int pageIndex = (page ?? 1);
            #endregion
            ViewBag.ID = id;
            (ViewBag.ID_PARENT, ViewBag.NAME_PARENT, ViewBag.Title, ViewBag.ItemType) = await repository.GetParentId(id);
            return View(await nodes.ToPagedListAsync(pageIndex, 5));
        }
        #endregion

        #region Details
        // GET: NODEs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NODE node = await repository.FindNodeAsync(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }
        #endregion

        #region Create
        // GET: NODEs/Create
        public ActionResult Create(int? id_parent, int? id_itemType)
        {
            var model = new NodeCreateViewModels { Id_parent = id_parent, Id_ItemTypeParent = id_itemType };
            ViewBag.ID_ITEMTYPE = repository.GetListItemTypes(model.Id_ItemTypeParent, model.Id_itemtype);
            return View(model);
        }

        // POST: NODEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NodeCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                var id_newnode = repository.InsertNode2(model.Id_parent, model.Id_itemtype, model.NameNode);
                return RedirectToAction(nameof(Edit), new { id = id_newnode, isCreated = true });
            }
            ViewBag.ID_ITEMTYPE = repository.GetListItemTypes(model.Id_ItemTypeParent, model.Id_itemtype);
            return View(model);
        }
        #endregion

        #region Edit
        private void SetupNodeEditViewModels(NodeEditViewModels model)
        {
            model.Dict = repository.ListDict();


            switch (model.Id_itemtype)
            {
                case 1954://исковое заявление
                    ViewBag.ListCodIska = repository.ListNode(3).OrderBy(n => n.STR_LABEL).ToList().Select(n => new SelectListItem { Text = n.STR_LABEL, Value = n.ID_NODE.ToString() });
                    ViewBag.ListSud = repository.ListNode(2).OrderBy(n => n.STR_LABEL).ToList().Select(n => new SelectListItem { Text = n.STR_LABEL, Value = n.ID_NODE.ToString() });
                    ViewBag.ListStoronaProc = repository.ListStoronaProc(model.Id_GrantParent).ToList().Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() });
                    break;
                case 2286://предмет иска

                    //ViewBag.ListKateg = repository.ListDict(2298).OrderBy(n => n.STR_NAME).ToList().Select(n => new SelectListItem { Text = n.STR_NAME });
                    //ViewBag.ListCodGrIskov = repository.ListDict(2288).OrderBy(n => n.STR_NAME).ToList().Select(n => new SelectListItem { Text = n.STR_NAME });
                    break;
            }
        }
        // GET: NODEs/Edit/5
        public async Task<ActionResult> Edit(int? id, bool isCreated = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NODE node = await repository.FindNodeAsync(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            //foreach (var item in node.ATTRS)
            //{
            //    foreach (var item1 in item.TREE_ATTRS)
            //    {
            //        item1.ITEMTYPE.SEARCH_TABLES
            //    }
            //}


            var model = new NodeEditViewModels
            {
                Id_node = node.ID_NODE,
                Id_itemtype = node.ID_ITEMTYPE,
                Id_parent = node.ID_PARENT,
                NameNode = node.STR_LABEL,
                Id_GrantParent=node.NODE2.ID_PARENT
            };
            var query = repository.GetNodeAttrs(model.Id_itemtype, model.Id_node).ToList();

            foreach (var item in query)
            {
                model.Attrs.Add(item.IdAttr, item);
            }

            SetupNodeEditViewModels(model);

            return View(model);
        }

        // POST: NODEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NodeEditViewModels model)
        {
            if (ModelState.IsValid)
            {
                NODE node = await repository.FindNodeAsync(model.Id_node);

                if ((node.ID_NODE == model.Id_node) && (node.ID_ITEMTYPE != model.Id_itemtype))
                {
                    repository.ChangeNodeType(model.Id_node, model.Id_itemtype);
                    return RedirectToAction(nameof(Edit), new { id = model.Id_node });
                }

                //db.Entry(nODE).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            //ViewBag.ID_NODE = new SelectList(db.EXTERNAL_DB, "ID_NODE", "BASE_XML", nODE.ID_NODE);
            //ViewBag.ID_ITEMTYPE = repository.GetListItemTypes(model.ID_ITEMTYPE);
            //ViewBag.ID_PARENT = new SelectList(db.NODEs, "ID_NODE", "STR_LABEL", nODE.ID_PARENT);
            //ViewBag.ID_USER = new SelectList(db.USERS, "ID_USER", "STR_LOGIN", nODE.ID_USER);
            return View(model);
        }
        #endregion
        //public async Task<JsonResult> GetIstec()
        //{

        //}
        #region Delete
        // GET: Regions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var node = await repository.FindNodeAsync(id);
            if (node == null)
            {
                throw new ApplicationException($"Не удалось загрузить элемент с ID '{id}'.");
                //return NotFound();
            }
            //var model = new RegionItemViewModel { Id = region.Id, Code = region.Code, Name = region.Name, Description = region.Description };
            return View(node);
        }

        // POST: NODEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await repository.RemoveNodeAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
