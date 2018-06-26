
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
    using System.ComponentModel.DataAnnotations;

    public partial class Matchday
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Matchday()
    {

        this.Matches = new HashSet<Match>();

    }


        [Display(Name = "Jornada")] public decimal matchday1 { get; set; }

        [Display(Name = "Temporada")] public short startYear { get; set; }

        [Display(Name = "Competencia")] public decimal competition { get; set; }

        [Display(Name = "Fecha")][DataType(DataType.Date)] public System.DateTime match_Date { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Match> Matches { get; set; }

    public virtual Season Season { get; set; }

}

}
