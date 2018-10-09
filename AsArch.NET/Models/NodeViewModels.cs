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
        [HiddenInput(DisplayValue = false)]
        public int? IdParent { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? IdGrantParent { get; set; }
        //[Display(Name = "Тип")]
        [HiddenInput(DisplayValue = false)]
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
        [HiddenInput(DisplayValue = false)]
        public int IdNode { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int IdAttr { get; set; }
        [HiddenInput(DisplayValue = false)]
        public List<NodeAttr> Attrs { get; set; }
        [HiddenInput(DisplayValue = false)]
        public IQueryable<DICTIONARy> Dict { get; set; }
    }
}