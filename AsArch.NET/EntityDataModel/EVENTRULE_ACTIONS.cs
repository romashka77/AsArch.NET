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
    
    public partial class EVENTRULE_ACTIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTRULE_ACTIONS()
        {
            this.EVENTRULE_CONDS = new HashSet<EVENTRULE_CONDS>();
        }
    
        public int ID_NODEACTION { get; set; }
        public int ID_EVENT { get; set; }
        public Nullable<int> ID_PARENT { get; set; }
        public Nullable<int> N_ORDER { get; set; }
        public int ACTIONTYPE { get; set; }
        public Nullable<int> N_OBJECT { get; set; }
        public string STR_MESSAGE { get; set; }
    
        public virtual EVENT EVENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENTRULE_CONDS> EVENTRULE_CONDS { get; set; }
    }
}
