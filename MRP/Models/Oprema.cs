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
    
    public partial class Oprema
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> DatumKupovine { get; set; }
        public Nullable<double> GodisnjaAmortizacija { get; set; }
        public Nullable<long> Dobavljac { get; set; }
        public Nullable<double> PocetnaCena { get; set; }
        public Nullable<double> TrenutnaVrednost { get; set; }
    
        public virtual Dobavljac Dobavljac1 { get; set; }
    }
}
