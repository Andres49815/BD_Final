
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FootballManagerApp.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Match_Referee
{

    public decimal match { get; set; }

    public decimal referee { get; set; }

    public decimal score { get; set; }

    public string position { get; set; }



    public virtual Match Match1 { get; set; }

    public virtual Position Position1 { get; set; }

    public virtual Referee Referee1 { get; set; }

}

}