﻿using AsArch.NET.EntityDataModel.Entytis;
using AsArch.NET.Interfaces;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsArch.NET.EntityDataModel
{
    public class Repository : IRepository, IDisposable
    {
        private DB_pfr_sap db;
        #region Конструктор
        public Repository(DB_pfr_sap context)
        {
            this.db = context ?? throw new ArgumentNullException(nameof(context));
            //лог запросов к БД
            //context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
        }
        #endregion
        #region Деструктор
        private bool disposedValue = false; // Для определения избыточных вызовов
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    if (db != null)
                    {
                        db.Dispose();
                        db = null;
                    }
                }
                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        //~Repository()
        //{
        //    // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //    Dispose(true);
        //}

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }
        #endregion
        public void DeleteSudZas(int? id_node, int? n_order)
        {
            DeleteTableDate(2091, id_node, n_order);
            DeleteTableChar(2091, id_node, n_order);
        }
        public void PostSudZas(int id, SudZas model)
        {
            UpdateTableChar(2091, id, model.Order, 0, model.N);
            if (!string.IsNullOrEmpty(model.DateValue))
                UpdateTableDate(2091, id, model.Order, 1, Convert.ToDateTime(model.DateValue));
            if (!string.IsNullOrEmpty(model.TimeValue))
                UpdateTableChar(2091, id, model.Order, 2, model.TimeValue);
            if (!string.IsNullOrEmpty(model.Comment))
                UpdateTableChar(2091, id, model.Order, 3, model.Comment);
            if (!string.IsNullOrEmpty(model.Isp))
                UpdateTableChar(2091, id, model.Order, 4, model.Isp);
            if (!string.IsNullOrEmpty(model.Sud))
                UpdateTableChar(2091, id, model.Order, 5, model.Sud);
        }
        public IQueryable<SudZas> GetSudZas(int id)
        {
            NODE node = FindNode(id);
            if (node == null)
            {
                return null;
            }
            var n = node.TABLEVAL_CHAR.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 0).Select(a => new { Id = a.ID_NODE, Order = a.N_ORDER, N = a.CHAR_VALUE });
            if (n.Count() == 0)
            {
                return new SudZas[0].AsQueryable();
            }
            var d = node.TABLEVAL_DATE.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 1).Select(a => new { Order = a.N_ORDER, DateValue = a.DATE_VALUE });
            var t = node.TABLEVAL_CHAR.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 2).Select(a => new { Order = a.N_ORDER, TimeValue = a.CHAR_VALUE });
            var c = node.TABLEVAL_CHAR.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 3).Select(a => new { Order = a.N_ORDER, Comment = a.CHAR_VALUE });
            var i = node.TABLEVAL_CHAR.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 4).Select(a => new { Order = a.N_ORDER, Isp = a.CHAR_VALUE });
            var s = node.TABLEVAL_CHAR.Where(a => a.ID_NODE == id && a.ID_ATTR == 2091 && a.ID_COL == 5).Select(a => new { Order = a.N_ORDER, Sud = a.CHAR_VALUE });
            var nd = from a in n
                     join b in d on a.Order equals b.Order into outer
                     from r in outer.DefaultIfEmpty()
                     select new { a.Id, a.Order, a.N, DateValue = r == null ? string.Empty : ((DateTime)r.DateValue).ToString("yyyy-MM-dd") };
            var ndt = from a in nd
                      join b in t on a.Order equals b.Order into outer
                      from r in outer.DefaultIfEmpty()
                      select new { a.Id, a.Order, a.N, a.DateValue, TimeValue = r == null ? string.Empty : r.TimeValue };
            var ndtc = from a in ndt
                       join b in c on a.Order equals b.Order into outer
                       from r in outer.DefaultIfEmpty()
                       select new { a.Id, a.Order, a.N, a.DateValue, a.TimeValue, Comment = r == null ? string.Empty : r.Comment };
            var ndtci = from a in ndtc
                        join b in i on a.Order equals b.Order into outer
                        from r in outer.DefaultIfEmpty()
                        select new { a.Id, a.Order, a.N, a.DateValue, a.TimeValue, a.Comment, Isp = r == null ? string.Empty : r.Isp };
            var res = from a in ndtci
                      join b in s on a.Order equals b.Order into outer
                      from r in outer.DefaultIfEmpty()
                      select new SudZas { /*Id = a.Id,*/ Order = a.Order, N = a.N, DateValue = a.DateValue, TimeValue = a.TimeValue, Comment = a.Comment, Isp = a.Isp, Sud = r == null ? string.Empty : r.Sud };
            return res.AsQueryable();
        }
        public async Task<NODE> RemoveNodeAsync(int id)
        {
            NODE node = await FindNodeAsync(id);
            var res = db.CanDeleteNode(node.ID_NODE, false);
            if (res > 0)
            {
                //Процедура удаления
                var res2 = db.DeleteNode(node.ID_NODE, false, null, null, null, null);
                //db.NODEs.Remove(node);
                //await db.SaveChangesAsync();
            }
            return node;
        }
        public async Task<int?> GetRegNum(int? id_parent, string year)
        {
            if (id_parent==null)
            {
                return null;
            }
            var query = db.Database.SqlQuery<int?>("SELECT " +
                "Max(CAST(substring(STR_LABEL, 1, CHARINDEX('-', STR_LABEL) - 1) AS int)) FROM NODE where ID_PARENT = @id_parent and " +
                "CHARINDEX(@year, STR_LABEL) > 0", new SqlParameter("id_parent", id_parent), new SqlParameter("year", year));
            return await query.SingleOrDefaultAsync();
        }
        public int RenameNode(int? id_node, string str_label)
        {
            return db.RenameNode(id_node, str_label, null);
        }
        public int ChangeNodeType(int? id_node, int? id_newtype)
        {
            //вызов функции
            return db.ChangeNodeType(id_node, id_newtype, null);
        }
        public int UpdateCharAttr(int? id_attr, int? id_node, string char_val)
        {
            return db.UpdateCharAttr(id_attr, id_node, char_val, null, null);
        }
        public int UpdateTextAttr(int? id_attr, int? id_node, string text_val)
        {
            return db.UpdateTextAttr(id_attr, id_node, null, text_val, null);
        }
        public void UpdateStorege(STORAGE storege)
        {
            storege.EDIT_TIME = DateTime.Now;
            db.Entry(storege).State = EntityState.Modified;
            db.SaveChanges();
        }
        public STORAGE GetStorege(int id_node, int order)
        {
            return db.STORAGEs.FirstOrDefault(n => n.ID_NODE == id_node && n.N_ORDER == order);
        }
        public void UpdateStorege(int id_node, string file_name, int order)
        {
            db.STORAGEs.Add(new STORAGE { ID_NODE = id_node, N_ORDER = order, STR_DOCFILE = file_name, EDIT_TIME = DateTime.Now });
            db.SaveChanges();
        }
        public int UpdateDateAttr(int? id_attr, int? id_node, Nullable<System.DateTime> date_val)
        {
            return db.UpdateDateAttr(id_attr, id_node, null, date_val, null);
        }
        public int UpdateFloatAttr(int? id_attr, int? id_node, double? float_val)
        {
            return db.UpdateFloatAttr(id_attr, id_node, null, float_val, null);
        }
        public int UpdateRefAttrs(int? id_attr, int? id_node1, int? id_node2)
        {
            return db.UpdateRefAttrs(id_attr, id_node1, 0, id_node2, null, null, null);
        }
        public int? InsertNode2(int? id_parent, int id_itemtype, string str_label)
        {
            ObjectParameter id_newnodeParameter = new ObjectParameter("id_newnode", typeof(int));

            var str_labelParameter = String.IsNullOrEmpty(str_label) ?

                new ObjectParameter("str_label", typeof(string)) :
                new ObjectParameter("str_label", str_label);

            db.InsertNode2(null, id_parent, id_itemtype, 0, null, str_labelParameter, null, null, 1 /*id_user*/, null, null, id_newnodeParameter, null);

            return (int)id_newnodeParameter.Value;
        }
        public NODE FindNode(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return db.NODEs.Find(id);
        }
        public async Task<NODE> FindNodeAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await db.NODEs.FindAsync(id);
        }
        public async Task<(int?, string, string, int?)> GetParentId(int? id = null)
        {
            var node = await db.NODEs.Select(n => new
            {
                Id = n.ID_NODE,
                n.ID_PARENT,
                n.NODE2.STR_LABEL,
                STR_LABEL2 = n.STR_LABEL,
                ItemType = n.ID_ITEMTYPE
            }).SingleOrDefaultAsync(n => n.Id == id);
            if (node is null)
                return (null, "Назад", "", null);
            else if (node.ID_PARENT is null)
                return (null, "Назад", node.STR_LABEL, node.ItemType);
            return (node.ID_PARENT, node.STR_LABEL, node.STR_LABEL2, node.ItemType);
        }
        public IQueryable<NodeAttr> GetNodeAttrs(int? id_itemtype, int id_node)
        {
            var query = db.Database.SqlQuery<NodeAttr>("select A.* " +
                ",T.OPTIONS as Options" +
                ", T.N_ORDER as NOrder" +
                ", R.ID_NODE2 as RefNode" +
                ", R.CHAR_VALUE as RefCharValue" +
                ", R.N_ORDER as RefNOrder" +
                ", CH.CHAR_VALUE" +
                ", DA.DATE_VALUE" +
                ", FL.FLOAT_VALUE" +
                ", I.INT_VALUE" +
                ", TE.TEXT_VALUE" +
                ", DE.ID_PARENT" +
                ", NOD.STR_LABEL as ParentName " +
                "from(" +
                "select DISTINCT B.ID_ATTR as IdAttr,B.IS_DEFAULT as IsDefault, C.STR_NAME as NameAttr, C.ID_ATTRTYPE as IdAttrType, C.IS_VIRTUAL as IsVirtual, D.STR_NAME as NameAttrType from CONTROLS_XML B, ATTRS C, ATTRTYPES D where B.ID_ATTR = C.ID_ATTR and B.IS_DEFAULT=0 and C.ID_ATTRTYPE = D.ID_ATTRTYPE and B.ID_TYPE = @id_type) A " +
                "left join ATTRVAL_CHAR CH on IdAttr = CH.ID_ATTR and CH.ID_NODE = @id_node " +
                "left join ATTRVAL_DATE DA on IdAttr = DA.ID_ATTR and DA.ID_NODE = @id_node " +
                "left join ATTRVAL_FLOAT FL on IdAttr = FL.ID_ATTR and FL.ID_NODE = @id_node " +
                "left join ATTRVAL_INT I on IdAttr = I.ID_ATTR and I.ID_NODE = @id_node " +
                "left join ATTRVAL_TEXT TE on IdAttr = TE.ID_ATTR and TE.ID_NODE = @id_node " +
                "left join TYPE_ATTR T on IdAttr = T.ID_ATTR and T.ID_TYPE = @id_type " +
                "left join REFATTRS R on IdAttr = R.ID_ATTR and R.ID_NODE1 = @id_node " +
                "left join DEFAULT_VALUES DE on IdAttr = DE.ID_ATTR and DE.ID_NODE=@id_node " +
                "left join NODE NOD on DE.ID_PARENT= NOD.ID_NODE " +
                " Order by NOrder, IdAttr ", new SqlParameter("id_type", id_itemtype), new SqlParameter("id_node", id_node));
            return query.AsQueryable<NodeAttr>();
        }
        public int UpdateTableChar(int? id_attr, int? id_node, int? n_order, int? id_col, string char_val/*, int? history_node, int? transFlag*/)
        {
            return db.UpdateTableChar(id_attr, id_node, n_order, id_col, char_val, null, null);
        }
        public int UpdateTableFloat(int? id_attr, int? id_node, int? n_order, int? id_col, double? float_val)
        {
            return db.UpdateTableFloat(id_attr, id_node, n_order, id_col, float_val, null, null);
        }
        public int UpdateTableDate(int? id_attr, int? id_node, int? n_order, int? id_col, DateTime? date_val)
        {
            return db.UpdateTableDate(id_attr, id_node, n_order, id_col, date_val, null, null);
        }
        //public IQueryable<TableData> GetTableData(int id_itemtype, int id_node, string nameAttr)
        //{
        //    var query = db.Database.SqlQuery<TableData>("select A.*" +
        //        ", T.OPTIONS as Options" +
        //        ", T.N_ORDER as NOrder" +
        //        ", TCON.STR_NAME as TabColName" +
        //        ", TCON.COLTYPE as TabColType" +
        //        ", TCON.INT_FROM" +
        //        ", TCON.INT_TO" +
        //        ", TCON.INT_WIDTH" +
        //        ", TCON.ID_COL as TabIdCol" +
        //        ", TCH.CHAR_VALUE as TabColCharValue" +
        //        ", TDAT.DATE_VALUE as TabColDateValue" +
        //        ", TINT.INT_VALUE as TabColInt" +
        //        ", TFLO.FLOAT_VALUE as TabColFloat" +
        //        ", isnull(TCH.N_ORDER, 0) + isnull(TFLO.N_ORDER, 0) + isnull(TDAT.N_ORDER, 0) + isnull(TINT.N_ORDER, 0) as TabOrder" +
        //        " from (select DISTINCT B.ID_ATTR as IdAttr" +
        //        ", B.IS_DEFAULT as IsDefault" +
        //        ", C.STR_NAME as NameAttr" +
        //        ", C.ID_ATTRTYPE as IdAttrType" +
        //        ", C.IS_VIRTUAL as IsVirtual" +
        //        ", D.STR_NAME as NameAttrType" +
        //        " from CONTROLS_XML B, ATTRS C, ATTRTYPES D" +
        //        " where B.ID_ATTR = C.ID_ATTR" +
        //        " and B.IS_DEFAULT = 0" +
        //        " and C.ID_ATTRTYPE = D.ID_ATTRTYPE" +
        //        " and B.ID_TYPE = @id_type) A" +
        //        " left join TYPE_ATTR T on IdAttr = T.ID_ATTR and T.ID_TYPE = @id_type" +
        //        " left join TABLECONFIG TCON on IdAttr = TCON.ID_ATTR" +
        //        " left join TABLEVAL_CHAR TCH on TCON.ID_COL = TCH.ID_COL and IdAttr = TCH.ID_ATTR and TCH.ID_NODE = @id_node" +
        //        " left join TABLEVAL_DATE TDAT on TCON.ID_COL = TDAT.ID_COL and IdAttr = TDAT.ID_ATTR and TDAT.ID_NODE = @id_node" +
        //        " left join TABLEVAL_INT TINT on TCON.ID_COL = TINT.ID_COL and IdAttr = TINT.ID_ATTR and TINT.ID_NODE = @id_node" +
        //        " left join TABLEVAL_FLOAT TFLO on TCON.ID_COL = TFLO.ID_COL and IdAttr = TFLO.ID_ATTR and TFLO.ID_NODE = @id_node" +
        //        " where IdAttrType = 7" +
        //        " and NameAttr = @nameAttr" +
        //        " and (TCH.CHAR_VALUE is not NULL" +
        //        " or TDAT.DATE_VALUE is not NULL" +
        //        " or TINT.INT_VALUE is not NULL " +
        //        " or TabColFloat is not NULL)" +
        //        " order by NOrder, IdAttr, TabOrder, TabIdCol;",
        //        new SqlParameter("id_type", id_itemtype), new SqlParameter("id_node", id_node), new SqlParameter("nameAttr", nameAttr));
        //    return query.AsQueryable();
        //}
        public IQueryable<NODE> ListNode(int? id_parent = null)
        {
            return db.NODEs.Where(n => n.ID_PARENT == id_parent);
        }
        public IQueryable<StoronaProc> ListStoronaProc(int? id_parent)
        {
            var query = db.Database.SqlQuery<StoronaProc>("SELECT B.STR_LABEL as Name,B.ID_NODE as Id FROM NODE A left join NODE B on A.ID_NODE = B.ID_PARENT where A.ID_PARENT = @id_parent  and B.ID_ITEMTYPE = 2159 ORDEr by B.STR_LABEL", new SqlParameter("id_parent", id_parent));
            return query.AsQueryable<StoronaProc>();
        }
        public StoronaProcParam StoronaProcParam(int id_node)
        {
            var query = db.Database.SqlQuery<StoronaProcParam>("select A.CHAR_VALUE as INN, B.CHAR_VALUE as Adres from (SELECT CHAR_VALUE FROM ATTRVAL_CHAR where ID_NODE = @id_node and ID_ATTR = 2170) A join ATTRVAL_CHAR B on B.ID_NODE = @id_node and B.ID_ATTR = 2165", new SqlParameter("id_node", id_node));
            return query.AsQueryable<StoronaProcParam>().FirstOrDefault();
        }
        public IQueryable<DICTIONARy> ListDict()
        {
            return db.DICTIONARIES;
        }
        public IQueryable<TABLELISTCONFIG> ListTabConfig()
        {
            return db.TABLELISTCONFIGs;
        }
        public SelectList GetListItemTypes(int? id_itemtype_parent = null, int? id_itemtype = null)
        {
            var itemType = db.ITEMTYPE_REF.Where(n => n.ID_ITEMTYPE1 == id_itemtype_parent && n.REF_MASK).Select(t => new
            {
                itemType = t.ID_ITEMTYPE2,
                name = t.ITEMTYPE1.STR_NAME
            });
            return new SelectList(itemType, "itemType", "name", id_itemtype);
        }
        public void DeleteTableChar(int? id_attr, int? id_node, int? n_order)
        {
            db.Database.ExecuteSqlCommand("DELETE TABLEVAL_CHAR WHERE ID_ATTR = @id_attr AND ID_NODE = @id_node AND N_ORDER = @n_order", new SqlParameter("id_attr", id_attr), new SqlParameter("id_node", id_node), new SqlParameter("n_order", n_order));
        }
        public void DeleteTableFloat(int? id_attr, int? id_node, int? n_order)
        {
            db.Database.ExecuteSqlCommand("DELETE TABLEVAL_FLOAT WHERE ID_ATTR = @id_attr AND ID_NODE = @id_node AND N_ORDER = @n_order", new SqlParameter("id_attr", id_attr), new SqlParameter("id_node", id_node), new SqlParameter("n_order", n_order));
        }
        public void DeleteTableDate(int? id_attr, int? id_node, int? n_order)
        {
            db.Database.ExecuteSqlCommand("DELETE TABLEVAL_DATE WHERE ID_ATTR = @id_attr AND ID_NODE = @id_node AND N_ORDER = @n_order", new SqlParameter("id_attr", id_attr), new SqlParameter("id_node", id_node), new SqlParameter("n_order", n_order));
        }
        public IQueryable<DocIsk> GetDocIsk(int id)
        {
            var a0 = db.FILESCONFIGs.Where(a => a.ID_TYPE == 1954);
            var b0 = db.STORAGEs.Where(b => b.ID_NODE == id);

            var query = from a in a0
                        join b in b0 on a.N_ORDER equals b.N_ORDER into c
                        from d in c.DefaultIfEmpty()
                        select new DocIsk { /*Id=id , */Order = a.N_ORDER, Filter = a.FILTER, Name = a.STR_NAME, DocFile = d.STR_DOCFILE==null ? string.Empty : d.STR_DOCFILE };

            return query;
        }
    }
}