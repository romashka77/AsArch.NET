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
    
    public partial class REFTREE_LINKS
    {
        public int ID_TREE { get; set; }
        public int N_LEVEL { get; set; }
        public int N_ORDER { get; set; }
        public Nullable<int> ID_LINKATTR { get; set; }
        public Nullable<int> ID_TOTYPE { get; set; }
        public Nullable<bool> NO_DIRECT { get; set; }
    }
}
