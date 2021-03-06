
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

    public partial class Team
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Team()
    {

        this.Coach_Team = new HashSet<Coach_Team>();

        this.LineUps = new HashSet<LineUp>();

        this.Matches = new HashSet<Match>();

        this.Matches1 = new HashSet<Match>();

        this.Player_Team = new HashSet<Player_Team>();

        this.Members = new HashSet<Member>();

    }


        [Display(Name = "Identificador")] public decimal iD { get; set; }

        [Display(Name = "Equipo")] public string abbreviation { get; set; }

        [Display(Name = "Equipo")] public string completeName { get; set; }

        [Display(Name = "Fecha de fundacion")] [DataType(DataType.Date)] public System.DateTime formation { get; set; }

        [Display(Name = "Federacion")] public string association { get; set; }



    public virtual Association Association1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Coach_Team> Coach_Team { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<LineUp> LineUps { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Match> Matches { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Match> Matches1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Player_Team> Player_Team { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Member> Members { get; set; }

}

}
