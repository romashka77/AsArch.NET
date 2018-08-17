using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using AsArch.NET.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Core.Objects;
using AsArch.NET.EntityDataModel.Entytis;

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
        public int ChangeNodeType(int? id_node, int? id_newtype)
        {
            //вызов функции
            var res = db.ChangeNodeType(id_node, id_newtype, null);
            return res;
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
                ID_PARENT = n.ID_PARENT,
                STR_LABEL = n.NODE2.STR_LABEL,
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
            var query = db.Database.SqlQuery<NodeAttr>("select A.* ,T.OPTIONS as Options, T.N_ORDER as NOrder, R.ID_NODE2 as RefNode, R.CHAR_VALUE as RefCharValue, R.N_ORDER as RefNOrder, CH.CHAR_VALUE, DA.DATE_VALUE, FL.FLOAT_VALUE, I.INT_VALUE, TE.TEXT_VALUE from(select DISTINCT B.ID_ATTR as IdAttr,B.IS_DEFAULT as IsDefault, C.STR_NAME as NameAttr, C.ID_ATTRTYPE as IdAttrType, C.IS_VIRTUAL as IsVirtual, D.STR_NAME as NameAttrType from CONTROLS_XML B, ATTRS C, ATTRTYPES D where B.ID_ATTR = C.ID_ATTR and B.IS_DEFAULT=0 and C.ID_ATTRTYPE = D.ID_ATTRTYPE and B.ID_TYPE = @id_type) A left join ATTRVAL_CHAR CH on IdAttr = CH.ID_ATTR and CH.ID_NODE = @id_node left join ATTRVAL_DATE DA on IdAttr = DA.ID_ATTR and DA.ID_NODE = @id_node left join ATTRVAL_FLOAT FL on IdAttr = FL.ID_ATTR and FL.ID_NODE = @id_node left join ATTRVAL_INT I on IdAttr = I.ID_ATTR and I.ID_NODE = @id_node left join ATTRVAL_TEXT TE on IdAttr = TE.ID_ATTR and TE.ID_NODE = @id_node left join TYPE_ATTR T on IdAttr = T.ID_ATTR and T.ID_TYPE = @id_type left join REFATTRS R on IdAttr = R.ID_ATTR and R.ID_NODE1 = @id_node order by NOrder", new SqlParameter("id_type", id_itemtype), new SqlParameter("id_node", id_node));
            return query.AsQueryable<NodeAttr>();
        }
        public IQueryable<NODE> ListNode(int? id_parent = null)
        {
            return db.NODEs.Where(n => n.ID_PARENT == id_parent);
        }
        public IQueryable<DICTIONARy> ListDict()
        {
            return db.DICTIONARIES;
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
    }
}