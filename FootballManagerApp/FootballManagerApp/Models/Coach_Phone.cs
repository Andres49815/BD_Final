
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
    
public partial class Coach_Phone
{

    public decimal coach_ID { get; set; }

    public decimal phoneNumber { get; set; }

    public string ph_Type { get; set; }



    public virtual Coach Coach { get; set; }

}

}
