//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ja2DataDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuxDataFlags
    {
        public AuxDataFlags()
        {
            this.AuxObjectAuxFlag = new HashSet<AuxObjectAuxFlag>();
        }
    
        public byte AuxDataFlagsId { get; set; }
        public string Name { get; set; }
        public byte Value { get; set; }
        public Nullable<int> Mask { get; set; }
    
        public virtual ICollection<AuxObjectAuxFlag> AuxObjectAuxFlag { get; set; }
    }
}