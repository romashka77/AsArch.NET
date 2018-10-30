using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class BaseJson
    {
        public int? IdNode { get; set; }
    }
    public class BaseJsonDelete : BaseJson
    {
        public string Ids { get; set; }
    }
}