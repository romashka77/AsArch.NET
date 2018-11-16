using System;
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
        void UpdateStorege(STORAGE storege);
        STORAGE GetStorege(int id_node, int order);
        void UpdateStorege(int id_node, string file_name, int order);
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
        NODE FindNode(int? id);
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

        int UpdateTableChar(int? id_attr, int? id_node, int? n_order, int? id_col, string char_val);
        void DeleteTableChar(int? id_attr, int? id_node, int? n_order);
        int UpdateTableFloat(int? id_attr, int? id_node, int? n_order, int? id_col, double? float_val);
        int UpdateTableDate(int? id_attr, int? id_node, int? n_order, int? id_col, DateTime? date_val);
        void DeleteTableFloat(int? id_attr, int? id_node, int? n_order);
        void DeleteTableDate(int? id_attr, int? id_node, int? n_order);
        IQueryable<TableData> GetTableData(int id_itemtype, int id_node, string nameAttr);
        IQueryable<DocIsk> GetDocIsk(int id);
        IQueryable<DICTIONARy> ListDict();
        IQueryable<TABLELISTCONFIG> ListTabConfig();
    }
}
