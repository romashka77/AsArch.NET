using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsArch.NET.EntityDataModel;
using AsArch.NET.EntityDataModel.Entytis;

namespace AsArch.NET.Models
{
    public class NodeViewModels
    {

        public int? Id_parent { get; set; }
        [Display(Name = "Тип")]
        public int Id_itemtype { get; set; }
        [Required(ErrorMessage = "Заполните поле {0}.")]
        [StringLength(256)]
        [Display(Name = "Наименование")]
        public string NameNode { get; set; }

    }

    public class NodeCreateViewModels : NodeViewModels
    {
        public int? Id_ItemTypeParent { get; set; }

    }
    public class NodeEditViewModels : NodeViewModels
    {
        public int Id_node;
        public int IdAttr;
        
        public Dictionary<int, NodeAttr> Attrs { get; set; }
        public IQueryable<DICTIONARy> Dict { get; set; }

        public NodeEditViewModels()
        {
            Attrs = new Dictionary<int, NodeAttr>();
        }
    }
}