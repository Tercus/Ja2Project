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
    
    public partial class JsdAuxData
    {
        public JsdAuxData()
        {
            this.AuxDataJsdAux = new HashSet<AuxDataJsdAux>();
        }
    
        public long JsdAuxDataId { get; set; }
        public long SlfRecordJsdId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public byte[] TileLocationData { get; set; }
    
        public virtual SlfRecordJsd SlfRecordJsd { get; set; }
        public virtual ICollection<AuxDataJsdAux> AuxDataJsdAux { get; set; }
    }
}