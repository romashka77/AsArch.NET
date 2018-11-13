using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class DocIsk
    {
        public int Id { get; set; }
        public int? Id_Node { get; set; }
        public string Name { get; set; }
        public string Filter { get; set; }
        public string DocFile { get; set; }
    }
}