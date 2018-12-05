using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class Base
    {
        //public int Id { get; set; }
    }
    public class BaseOrder : Base
    {
        public int Order { get; set; }
    }
    public class BaseOrders : Base
    {
        public string Orders { get; set; }
    }
}