//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AsArch.NET.EntityDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SEARCH_TABLES
    {
        public int ID_ATTR { get; set; }
        public Nullable<int> ID_SEARCHTYPE { get; set; }
        public int N_ORDER { get; set; }
        public string STR_COND { get; set; }
        public string STR_VALUE { get; set; }
        public int OP_COND { get; set; }
        public Nullable<bool> IS_AND { get; set; }
        public Nullable<int> ID_STATATTR { get; set; }
        public Nullable<bool> IS_TREE { get; set; }
    
        public virtual ATTR ATTR { get; set; }
        public virtual ITEMTYPE ITEMTYPE { get; set; }
    }
}
