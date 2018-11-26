using AsArch.NET.EntityDataModel;
using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;
using AsArch.NET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using X.PagedList;

namespace AsArch.NET.Controllers
{
    //[Authorize]
    public class NodesController : Controller
    {
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
            var model = new NodeCreateViewModels { IdParent = id_parent, IdItemTypeParent = id_itemType };
            ViewBag.ID_ITEMTYPE = repository.GetListItemTypes(model.IdItemTypeParent, model.IdItemType);
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
                var id_newnode = repository.InsertNode2(model.IdParent, model.IdItemType, model.NameNode);
                return RedirectToAction(nameof(Edit), new { id = id_newnode, isCreated = true });
            }
            ViewBag.ID_ITEMTYPE = repository.GetListItemTypes(model.IdItemTypeParent, model.IdItemType);
            return View(model);
        }
        #endregion

        #region DocIsk
        //[HttpGet]
        //public JsonResult DocIskLoad(BaseOrder docisk)
        //{
        //    var storege = repository.GetStorege((int)docisk.Id, (int)docisk.Order);
        //    return Json(storege, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        //[Route("Upload")]
        public ActionResult DocIskUpload(int? id, int? order, string load)
        {
            //int Order = int.Parse(Request.Params["Order"]);
            //путь к файлу
            string filePath = Server.MapPath(Path.Combine("~/Files/", load));
            FileStream fs = new FileStream(filePath, FileMode.Open);
            string fileType = $"application/{Path.GetExtension(load).TrimStart('.')}";

            return File(/*filePath*/fs, fileType, load);
        }
        //[HttpDelete]
        //public ActionResult DocIskUpload(int? id, int? order, string delete)
        //{
        //    return null;
        //}
        [HttpPost]
        //[Route("Upload")]
        public ActionResult DocIskUpload(BaseOrder docisk)
        {
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    var upload = Request.Files[file];
                    if (upload != null)
                    {
                        // сохраняем файл в папку Files в проекте
                        var fileName = $"{docisk.Id}_doc{docisk.Order + 1}{Path.GetExtension(Path.GetFileName(upload.FileName))}";
                        var filePath = Path.Combine("~/Files/", fileName);
                        //string fileName = $"~/Files/";
                        upload.SaveAs(Server.MapPath(filePath));
                        var storege = repository.GetStorege((int)docisk.Id, (int)docisk.Order);
                        if (storege == null)
                        {
                            repository.UpdateStorege((int)docisk.Id, fileName, (int)docisk.Order);
                        }
                        else
                        {
                            storege.STR_DOCFILE = fileName;
                            repository.UpdateStorege(storege);
                        }
                        return Json(new
                        {
                            success = true,
                            result = "error",
                            data = new
                            {
                                fileName
                            }
                        });
                    }
                }
            }
            return Json(new { error = "Нужно загрузить файл", success = false });
        }

        private IEnumerable<DocIsk> GetDocIsk(int id)
        {
            var data = repository.GetDocIsk(id).ToList();
            return data;
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetDocIskJson(int id)
        {
            return Json(GetDocIsk(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SudZas
        [HttpDelete]
        public ActionResult DeleteSudZas(BaseOrders model)
        {
            if (ModelState.IsValid)
            {
                string[] ids = model.Orders.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in ids)
                {
                    var i = int.Parse(item);
                    repository.DeleteTableDate(2091, model.Id, i);
                    repository.DeleteTableChar(2091, model.Id, i);
                }
            }
            return Content("Success :)");
        }
        [HttpPost]
        public ActionResult PostSudZas(SudZas model)
        {
            if (ModelState.IsValid)
            {
                if (model.Order == null)
                {
                    if (GetSudZas(model.Id).Where(n => n.Isp != null || n.Sud != null || n.TimeValue != null || n.DateValue != null || n.Comment != null || n.N != null).Count() == 0)
                    {
                        model.Order = 0;
                    }
                    else
                    {
                        model.Order = GetSudZas(model.Id).Max(n => n.Order) + 1;
                    }
                    model.N = (model.Order + 1).ToString();
                }
                repository.UpdateTableChar(2091, model.Id, model.Order, 0, model.N);
                repository.UpdateTableDate(2091, model.Id, model.Order, 1, Convert.ToDateTime(model.DateValue));
                repository.UpdateTableChar(2091, model.Id, model.Order, 2, model.TimeValue);
                repository.UpdateTableChar(2091, model.Id, model.Order, 3, model.Comment);
                repository.UpdateTableChar(2091, model.Id, model.Order, 4, model.Isp);
                repository.UpdateTableChar(2091, model.Id, model.Order, 5, model.Sud);
            }
            return Content("Success :)");
        }
        private IEnumerable<SudZas> GetSudZas(int id)
        {
            var data = repository.GetTableData(1954, id, "Предмет иска судебных заседаний").ToList();
            var n = data.Where(m => m.TabIdCol == 0);
            var d = data.Where(m => m.TabIdCol == 1);
            var t = data.Where(m => m.TabIdCol == 2);
            var c = data.Where(m => m.TabIdCol == 3);
            var i = data.Where(m => m.TabIdCol == 4);
            var s = data.Where(m => m.TabIdCol == 5);
            var t0 = n.Join(d, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, Id = a.TabColCharValue, DateValue = b.TabColDateValue });
            var t1 = t0.Join(t, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, Id = a.Id, DateValue = a.DateValue, TimeValue = b.TabColCharValue });
            var t2 = t1.Join(c, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, Id = a.Id, DateValue = a.DateValue, TimeValue = a.TimeValue, Comment = b.TabColCharValue });
            var t3 = t2.Join(i, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, Id = a.Id, DateValue = a.DateValue, TimeValue = a.TimeValue, Comment = a.Comment, Isp = b.TabColCharValue });

            var res = t3.Join(s, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, Id = a.Id, DateValue = a.DateValue, TimeValue = a.TimeValue, Comment = a.Comment, Isp = a.Isp, Sud = b.TabColCharValue }).Select(a => new SudZas { Id = id, Order = a.TabOrder, N = a.Id, DateValue = ((DateTime)a.DateValue).ToString("yyyy-MM-dd"), TimeValue = a.TimeValue, Comment = a.Comment, Isp = a.Isp, Sud = a.Sud });
            return res;
        }
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetSudZasJson(int id)
        {
            return Json(GetSudZas(id), JsonRequestBehavior.AllowGet);
        }
        #endregion 
        #region DopPredIsk
        private IEnumerable<DopPredIsk> GetDopPredIsk(int id)
        {
            var data = repository.GetTableData(1954, id, "Дополнительный предмет иска").ToList();
            var n = data.Where(m => m.TabIdCol == 0);
            var name = data.Where(m => m.TabIdCol == 1);
            var prim = data.Where(m => m.TabIdCol == 2);

            var t = n.Join(name, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, N = a.TabColFloat, NameIsk = b.TabColCharValue });
            var res = t.Join(prim, a => a.TabOrder, b => b.TabOrder, (a, b) => new { TabOrder = a.TabOrder, N = a.N, NameIsk = a.NameIsk, Prim = b.TabColCharValue }).Select(a => new DopPredIsk { Id = id, N = (int?)a.N, Order = a.TabOrder, Name = a.NameIsk, Comment = a.Prim });
            return res;
        }
        [OutputCache(Location = OutputCacheLocation.None)]
        [HttpGet]
        public ActionResult GetDopPredIskJson(int id)
        {
            return Json(GetDopPredIsk(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostDopPredIsk(DopPredIsk model)
        {
            if (ModelState.IsValid)
            {
                if (model.Order == null)
                {
                    if (GetDopPredIsk(model.Id).Where(n => n.N != null || n.Name != null || n.Comment != null).Count() == 0)
                    {
                        model.Order = 0;
                    }
                    else
                    {
                        model.Order = GetDopPredIsk(model.Id).Max(n => n.Order) + 1;
                    }
                    model.N = model.Order + 1;
                }
                repository.UpdateTableFloat(2153, model.Id, model.Order, 0, model.N);
                repository.UpdateTableChar(2153, model.Id, model.Order, 1, model.Name);
                repository.UpdateTableChar(2153, model.Id, model.Order, 2, model.Comment);
                return Content("Success :)");
            }
            return Content("Error :(");
        }

        [HttpDelete]
        public ActionResult DeleteDopPredIsk(BaseOrders model)
        {
            if (ModelState.IsValid)
            {
                string[] ids = model.Orders.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in ids)
                {
                    var i = int.Parse(item);
                    repository.DeleteTableChar(2153, model.Id, i);
                    repository.DeleteTableFloat(2153, model.Id, i);
                }
            }
            return Content("Success :)");
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetListTabConfig()
        {
            var data = repository.ListTabConfig().OrderBy(n => n.STR_NAME).ToList().Select(n => n.STR_NAME);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetListDictJson(int id)
        {
            var data = repository.ListDict().Where(d => d.ID_ATTR == id).OrderBy(n => n.STR_NAME).ToList().Select(n => n.STR_NAME);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Edit
        private void SetupNodeEditViewModels(NodeEditViewModels model)
        {
            model.Dict = repository.ListDict();
            switch (model.IdItemType)
            {
                case 1954://исковое заявление
                    ViewBag.ListCodIska = repository.ListNode(3).OrderBy(n => n.STR_LABEL);
                    ViewBag.ListSud = repository.ListNode(2).OrderBy(n => n.STR_LABEL);
                    ViewBag.ListStoronaProc = repository.ListStoronaProc(model.IdGrantParent).ToList();
                    break;
                case 2286://предмет иска

                    //ViewBag.ListKateg = repository.ListDict(2298).OrderBy(n => n.STR_NAME).ToList().Select(n => new SelectListItem { Text = n.STR_NAME });
                    //ViewBag.ListCodGrIskov = repository.ListDict(2288).OrderBy(n => n.STR_NAME).ToList().Select(n => new SelectListItem { Text = n.STR_NAME });
                    break;
            }
        }
        private void SetNodeAttrs(NodeEditViewModels model)
        {
            var ur_lico = "ur-lico";
            var fiz_lico = "fiz-lico";
            //if (model.IdItemType == 1954)
            if (model.ItemType == "Исковое заявление")
            {
                model.Attrs = new List<NodeAttr>();
                var rep = repository.GetNodeAttrs(model.IdItemType, model.IdNode).ToList();
                //0Регистрационный номер
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Регистрационный номер"/* IdAttr == 2141*/));
                //1Регистрационная дата
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Регистрационная дата"/* IdAttr == 2134*/));
                //2Номер дела
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Номер дела"/*IdAttr==1958*/));
                //3Дата принятия иска
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Дата принятия иска"/*IdAttr == 2506*/));
                //4
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Название управления" /*IdAttr == 2203*/));
                //5
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Название отделения" /*IdAttr == 2205*/));
                //6
                model.Attrs.Add(rep.SingleOrDefault(n => n./*NameAttr== "Название управление района\города"*/IdAttr == 2207));
                //7Категория споров
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Категория споров" /*IdAttr == 2298*/));
                //8Исковое заявление - код иска
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Исковое заявление - код иска" /*IdAttr == 2369*/));
                //9Наименование предмета иска
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Наименование предмета иска" /*IdAttr == 1964*/));

                //10Код предмета иска Ссылка 1:1 сделать кнопку
                model.Attrs.AddRange(rep.Where(n => n.NameAttr == "Код предмета иска Ссылка 1:1"/* IdAttr == 2299*/));

                //11Название суда
                model.Attrs.AddRange(rep.Where(n => n.NameAttr == "Наименование суда Ссылка 1:1" /*IdAttr == 2253*/));
                //12Вид суда
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Вид суда" /*IdAttr == 1957*/));
                //13Адрес суда
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Адрес суда" /*IdAttr == 2158*/));
                //14Регион суда(текст)
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Регион суда(текст)" /*IdAttr == 2272*/));
                //15Сумма иска
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Сумма иска" /*IdAttr == 2235*/));
                //16Исполнитель
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Исполнитель" /*IdAttr == 2234*/));
                //17Истец
                model.Attrs.AddRange(rep.Where(n => n.NameAttr == "Истец Контрагенты Ссылка 1:1" /*IdAttr == 2263*/));
                //18Истец ИНН
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Истец ИНН" /*IdAttr == 2241*/));
                //19Истец Адрес
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Истец Адрес " /*IdAttr == 2228*/));
                //20Ответчик
                model.Attrs.AddRange(rep.Where(n => n.NameAttr == "Ответчик Контрагенты Ссылка1:1" /*IdAttr == 2264*/));
                //21Ответчик ИНН
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Ответчик ИНН" /*IdAttr == 2242*/));
                //22Ответчик Адрес
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Ответчик Адрес" /*IdAttr == 2229*/));
                //23 3-лицо
                model.Attrs.AddRange(rep.Where(n => n.NameAttr == "3-лицо Контрагенты Ссылка1:1" /*IdAttr == 2266*/));
                //24 3-лицо ИНН
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2243));
                //25 3-лицо Адрес
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2230));
                //26 Примечание
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 969));
                //27
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2011));
                //28
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2022));
                //29
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2027));
                //30
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1993));
                //31
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2010));
                //32
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2023));
                //33
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2028));
                //34
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1994));
                //35
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2012));
                //36
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2025));
                //37
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2029));
                //38
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1995));
                //39
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2013));
                //40
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2024));
                //41
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2030));
                //42
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1997));
                //43
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2015));
                //44
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2034));
                //45
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2035));
                //46
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1996));
                //47
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2014));
                //48
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2032));
                //49
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2033));
                //50
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1998));
                //51
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2019));
                //52
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2038));
                //53
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2039));
                //54
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 1999));
                //55
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2018));
                //56
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2036));
                //57
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2037));
                //58
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2511));
                //59
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2512));
                //60
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2513));
                //61
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2514));
                //62
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2310));
                //63
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2311));
                //64
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2312));
                //65
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2313));
                //66
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2519));
                //67
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2520));
                //68
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2521));
                //69
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2523));
                //70
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2306));
                //71
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2307));
                //72
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2308));
                //73
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2309));
                //74
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2515));
                //75
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2516));
                //76
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2517));
                //77
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2518));
                //78
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2314));
                //79
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2315));
                //80
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2316));
                //81
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2317));
                //82
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2726));
                //83
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2438));
                //84
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2000));
                //85
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2020));
                //86
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2042));
                //87
                model.Attrs.Add(rep.SingleOrDefault(n => n.IdAttr == 2040));
                var t = rep.Where(n => n.IdAttr == 2153);
            }
            //if (model.IdItemType == 2159)
            else if (model.ItemType == "Сторона процесса")
            {
                model.Attrs = new List<NodeAttr>();
                var rep = repository.GetNodeAttrs(model.IdItemType, model.IdNode).ToList();
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Тип контрагента"));//2161

                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Регистрационный №"));
                

                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Наименование Контрагента полное"));
                model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Наименование Контрагента краткое "));
                model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "ФИО Контрагента"));
                model.Attrs.Last().NameClass = fiz_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Паспортные данные контрагента"));
                model.Attrs.Last().NameAttr = "Дата и место рождения";
                model.Attrs.Last().NameClass = fiz_lico;

                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "ОПФ"));
                model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Адрес фактический"));
                //model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Адрес юридический"));
                //model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Last().NameAttr = "Адрес";


                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Наименование банка"));
                //model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Адрес банка"));
                model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "БИК"));
                //model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Р/с"));
                //model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "К/с"));
                //model.Attrs.Last().NameClass = ur_lico;
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "ИНН"));
                model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "ОГРН"));
                //model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "КПП"));
                //model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Телефон контрагента"));
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Сотовый телефон контрагента"));
                //model.Attrs.Last().NameClass = fiz_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Факс контрагента"));
                //model.Attrs.Last().NameClass = ur_lico;
                //model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "e-mail контрагента"));
                model.Attrs.Add(rep.SingleOrDefault(n => n.NameAttr == "Примечание"));
            }
            else
            {

                model.Attrs = repository.GetNodeAttrs(model.IdItemType, model.IdNode).ToList();
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
            var model = new NodeEditViewModels
            {
                IdNode = node.ID_NODE,
                IdItemType = node.ID_ITEMTYPE,
                IdParent = node.ID_PARENT,
                NameNode = node.STR_LABEL,
                IdGrantParent = node.NODE2.ID_PARENT,
                ItemType = node.ITEMTYPE.STR_NAME
            };
            SetNodeAttrs(model);
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
                NODE node = await repository.FindNodeAsync(model.IdNode);
                if (node.ID_NODE == model.IdNode)
                {
                    if (node.ID_ITEMTYPE != model.IdItemType)
                    {
                        repository.ChangeNodeType(model.IdNode, model.IdItemType);
                        return RedirectToAction(nameof(Edit), new { id = model.IdNode });
                    }
                    if (node.STR_LABEL != model.NameNode)
                    {
                        repository.RenameNode(model.IdNode, model.NameNode);
                    }
                    for (int i = 0; i < model.Attrs.Count; i++)
                    {
                        switch (model.Attrs[i].IdAttrType)
                        {
                            case 0:
                                repository.UpdateCharAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].CHAR_VALUE);
                                break;
                            case 2:
                                repository.UpdateTextAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].TEXT_VALUE);
                                break;
                            case 3:
                                if (model.Attrs[i].CHAR_VALUE != null)
                                {
                                    repository.UpdateCharAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].CHAR_VALUE);
                                }
                                break;
                            case 4:
                                repository.UpdateDateAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].DATE_VALUE);
                                break;
                            case 8:
                                repository.UpdateRefAttrs(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].RefNode);
                                break;
                            case 11:
                                repository.UpdateFloatAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].FLOAT_VALUE);
                                break;
                            default:
                                repository.UpdateCharAttr(model.Attrs[i].IdAttr, model.IdNode, model.Attrs[i].CHAR_VALUE);
                                break;
                        }
                    }
                }
            }
            SetupNodeEditViewModels(model);
            return View(model);
        }
        #endregion
        public PartialViewResult StoronaProcParam(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return PartialView(new StoronaProcParam { Adres = "", INN = "" });
            }

            return PartialView(new StoronaProcParam { Adres = "Adres", INN = "INN" }); ;

            //return Json(new { ip = Dns.GetHostName() }, JsonRequestBehavior.AllowGet);
        }
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
