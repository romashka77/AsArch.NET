using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AsArch.NET.EntityDataModel;
using AsArch.NET.EntityDataModel.Entytis;

namespace AsArch.NET.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        /// Возвращает набор дочерних узлов (IQueryable<NODE>) по id узла
        /// </summary>
        /// <param name="id_parent"></param>
        /// <returns></returns>
        IQueryable<NODE> ListNode(int? id_parent = null);
        IQueryable<StoronaProc> ListStoronaProc(int? id_parent);
        StoronaProcParam StoronaProcParam(int id_node);
        /// <summary>
        ///  Возвращает id, name родителя, name и тип узла по id узла
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(int?, string, string, int?)> GetParentId(int? id = null);
        Task<NODE> FindNodeAsync(int? id);

        /// <summary>
        /// По типу узла возвращает набор атрибутов для запонения
        /// </summary>
        /// <param name="id_itemtype"></param>
        /// <returns></returns>
        IQueryable<NodeAttr> GetNodeAttrs(int? id_itemtype, int id_node);
        //IQueryable<NodeValueString> GetNodeValueString(int? id_node);


        /// <summary>
        /// Возвращает список типов узлов
        /// </summary>
        /// <param name="id_itemtype"></param>
        /// <returns></returns>
        SelectList GetListItemTypes(int? id_itemtype_parent = null, int? id_itemtype = null);
        int? InsertNode2(int? id_parent, int id_itemtype, string str_label);
        Task<NODE> RemoveNodeAsync(int id);
        int ChangeNodeType(int? id_node, int? id_newtype);
        int RenameNode(int? id_node, string str_label);
        int UpdateCharAttr(int? id_attr, int? id_node, string char_val);
        int UpdateTextAttr(int? id_attr, int? id_node, string text_val);
        int UpdateDateAttr(int? id_attr, int? id_node, Nullable<System.DateTime> date_val);
        int UpdateFloatAttr(int? id_attr, int? id_node, double? float_val);
        int UpdateRefAttrs(int? id_attr, int? id_node1, int? id_node2);
        IQueryable<DICTIONARy> ListDict();
    }
}
