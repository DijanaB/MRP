//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MRP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VremenaProizvodnje
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> DatumVreme { get; set; }
        public Nullable<long> FazaProizvodnje { get; set; }
        public Nullable<long> RadnoMesto { get; set; }
        public Nullable<long> Proizvod { get; set; }
    
        public virtual FazaProizvodnje FazaProizvodnje1 { get; set; }
        public virtual Proizvod Proizvod1 { get; set; }
        public virtual RadnoMesto RadnoMesto1 { get; set; }
    }
}
