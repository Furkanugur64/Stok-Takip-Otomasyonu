//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UĞUR_PAZARLAMA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ILLER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_ILLER()
        {
            this.TBL_ILCELER = new HashSet<TBL_ILCELER>();
        }
    
        public int ID { get; set; }
        public string SEHIR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ILCELER> TBL_ILCELER { get; set; }
    }
}
