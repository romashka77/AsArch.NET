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
    
    public partial class ATTR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATTR()
        {
            this.ATTRVAL_CHAR = new HashSet<ATTRVAL_CHAR>();
            this.ATTRVAL_DATE = new HashSet<ATTRVAL_DATE>();
            this.ATTRVAL_FLOAT = new HashSet<ATTRVAL_FLOAT>();
            this.ATTRVAL_IMAGE = new HashSet<ATTRVAL_IMAGE>();
            this.ATTRVAL_INT = new HashSet<ATTRVAL_INT>();
            this.ATTRVAL_LV = new HashSet<ATTRVAL_LV>();
            this.ATTRVAL_TEXT = new HashSet<ATTRVAL_TEXT>();
            this.CHILD_ATTRS = new HashSet<CHILD_ATTRS>();
            this.CHILD_ATTRS1 = new HashSet<CHILD_ATTRS>();
            this.CHILD_ATTRS2 = new HashSet<CHILD_ATTRS>();
            this.CHILD_USER_ATTRS = new HashSet<CHILD_USER_ATTRS>();
            this.CHILD_USER_ATTRS1 = new HashSet<CHILD_USER_ATTRS>();
            this.EVENTS = new HashSet<EVENT>();
            this.EXTERNAL_CHILD_ATTRS = new HashSet<EXTERNAL_CHILD_ATTRS>();
            this.EXTERNAL_CHILD_ATTRS1 = new HashSet<EXTERNAL_CHILD_ATTRS>();
            this.EXTERNAL_LINKS = new HashSet<EXTERNAL_LINKS>();
            this.FACECONFIGs = new HashSet<FACECONFIG>();
            this.REFATTRS = new HashSet<REFATTR>();
            this.REFATTRS_USER = new HashSet<REFATTRS_USER>();
            this.SEARCH_TABLES = new HashSet<SEARCH_TABLES>();
            this.TABLE_SORTING = new HashSet<TABLE_SORTING>();
            this.TABLECONFIGs = new HashSet<TABLECONFIG>();
            this.TABLELISTCONFIGs = new HashSet<TABLELISTCONFIG>();
            this.TABLEVAL_CHAR = new HashSet<TABLEVAL_CHAR>();
            this.TABLEVAL_DATE = new HashSet<TABLEVAL_DATE>();
            this.TABLEVAL_FLOAT = new HashSet<TABLEVAL_FLOAT>();
            this.TABLEVAL_INT = new HashSet<TABLEVAL_INT>();
            this.TREE_ATTRS = new HashSet<TREE_ATTRS>();
            this.TYPE_ATTR = new HashSet<TYPE_ATTR>();
            this.NODEs = new HashSet<NODE>();
        }
    
        public int ID_ATTR { get; set; }
        public string STR_NAME { get; set; }
        public int ID_ATTRTYPE { get; set; }
        public Nullable<bool> IS_VIRTUAL { get; set; }
    
        public virtual ATTR_FUNCTIONS ATTR_FUNCTIONS { get; set; }
        public virtual ATTRTYPE ATTRTYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_CHAR> ATTRVAL_CHAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_DATE> ATTRVAL_DATE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_FLOAT> ATTRVAL_FLOAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_IMAGE> ATTRVAL_IMAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_INT> ATTRVAL_INT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_LV> ATTRVAL_LV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTRVAL_TEXT> ATTRVAL_TEXT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHILD_ATTRS> CHILD_ATTRS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHILD_ATTRS> CHILD_ATTRS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHILD_ATTRS> CHILD_ATTRS2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHILD_USER_ATTRS> CHILD_USER_ATTRS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHILD_USER_ATTRS> CHILD_USER_ATTRS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENT> EVENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTERNAL_CHILD_ATTRS> EXTERNAL_CHILD_ATTRS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTERNAL_CHILD_ATTRS> EXTERNAL_CHILD_ATTRS1 { get; set; }
        public virtual EXTERNAL_CONFIG EXTERNAL_CONFIG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTERNAL_LINKS> EXTERNAL_LINKS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACECONFIG> FACECONFIGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REFATTR> REFATTRS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REFATTRS_USER> REFATTRS_USER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEARCH_TABLES> SEARCH_TABLES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLE_SORTING> TABLE_SORTING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLECONFIG> TABLECONFIGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLELISTCONFIG> TABLELISTCONFIGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLEVAL_CHAR> TABLEVAL_CHAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLEVAL_DATE> TABLEVAL_DATE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLEVAL_FLOAT> TABLEVAL_FLOAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLEVAL_INT> TABLEVAL_INT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TREE_ATTRS> TREE_ATTRS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TYPE_ATTR> TYPE_ATTR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NODE> NODEs { get; set; }
    }
}
