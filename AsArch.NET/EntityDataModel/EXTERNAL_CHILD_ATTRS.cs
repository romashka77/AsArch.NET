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
    
    public partial class EXTERNAL_CHILD_ATTRS
    {
        public int ID_TYPE { get; set; }
        public int ID_ATTR { get; set; }
        public int ID_REFATTR { get; set; }
        public string STR_PARENTATTR { get; set; }
    
        public virtual ATTR ATTR { get; set; }
        public virtual ATTR ATTR1 { get; set; }
        public virtual ITEMTYPE ITEMTYPE { get; set; }
    }
}
