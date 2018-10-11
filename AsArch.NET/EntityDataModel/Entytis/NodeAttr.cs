using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class NodeAttr
    {
        //public string Alias { get; set; }
        public int IdAttr { get; set; }
        public int IsDefault { get; set; }

        public string NameAttr { get; set; }
        public int IdAttrType { get; set; }
        public bool? IsVirtual { get; set; }
        public string NameAttrType { get; set; }
        public string CHAR_VALUE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DATE_VALUE { get; set; }
        public double? FLOAT_VALUE { get; set; }
        public int? INT_VALUE { get; set; }
        public string TEXT_VALUE { get; set; }

        public int? Options { get; set; }
        public int? NOrder { get; set; }

        public int? RefNode { get; set; }
        public string RefCharValue { get; set; }
        public int? RefNOrder { get; set; }

        public int? ID_PARENT { get; set; }
        public string ParentName { get; set; }

        public string TabColName { get; set; }
        public int? TabColType { get; set; }
        public int? INT_FROM { get; set; }
        public int? INT_TO { get; set; }
        public int? INT_WIDTH { get; set; }
        public int? TabIdCol { get; set; }
        public string TabColCharValue { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TabColDateValue { get; set; }
        public int? TabColInt { get; set; }
        public double? TabColFloat { get; set; }
        public int TabOrder { get; set; }
    }
}