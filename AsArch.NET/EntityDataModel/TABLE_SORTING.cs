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
    
    public partial class TABLE_SORTING
    {
        public int ID_ATTR { get; set; }
        public int ID_COL { get; set; }
        public int ID_ORDER { get; set; }
    
        public virtual ATTR ATTR { get; set; }
        public virtual TABLECONFIG TABLECONFIG { get; set; }
    }
}
