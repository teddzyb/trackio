//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace trackio
{
    using System;
    using System.Collections.Generic;
    
    public partial class HydrationLog
    {
        public int HydrationLogID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Glasses { get; set; }
        public string Description { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
