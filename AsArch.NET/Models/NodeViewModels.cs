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
        public int IdNode { get; set; }
        public int? IdParent { get; set; }
        public int? IdGrantParent { get; set; }
        [Display(Name = "Тип")]
        public int IdItemType { get; set; }
        [Required(ErrorMessage = "Заполните поле {0}.")]
        [StringLength(256)]
        [Display(Name = "Наименование")]
        public string NameNode { get; set; }

    }

    public class NodeCreateViewModels : NodeViewModels
    {
        public int? IdItemTypeParent { get; set; }

    }
    public class NodeEditViewModels : NodeViewModels
    {
        
        public int IdAttr { get; set; }
        public Dictionary<int, NodeAttr> Attrs { get; set; }
        public IQueryable<DICTIONARy> Dict { get; set; }

        public NodeEditViewModels()
        {
            Attrs = new Dictionary<int, NodeAttr>();
        }
    }
}