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
    
    public partial class AuxDataJsdAux
    {
        public long AuxDataJsdAuxId { get; set; }
        public long AuxObjectDataId { get; set; }
        public long JsdAuxDataId { get; set; }
    
        public virtual AuxObjectData AuxObjectData { get; set; }
        public virtual JsdAuxData JsdAuxData { get; set; }
    }
}
