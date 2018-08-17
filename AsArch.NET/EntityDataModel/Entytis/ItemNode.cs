using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class BaesItemNode
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }

    public class ItemNode: BaesItemNode
    {
        public int? Id_parent { get; set; }
        public byte[] Icon { get; set; }
        public string TypeName { get; set; }
    }
}