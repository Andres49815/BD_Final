
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

    public partial class Player
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Player()
    {

        this.LineUps = new HashSet<LineUp>();

        this.Player_Direction = new HashSet<Player_Direction>();

        this.Player_Email = new HashSet<Player_Email>();

        this.Player_Team = new HashSet<Player_Team>();

        this.PlayerPositions = new HashSet<PlayerPosition>();

    }


    public decimal iD { get; set; }

        [Display(Name = "Nombre del jugador")] public string firstName { get; set; }

        [Display(Name = "Apellido")] public string lastName { get; set; }

        [Display(Name = "Peso")] public Nullable<decimal> weight { get; set; }

        [Display(Name = "Altura")] public Nullable<decimal> height { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<LineUp> LineUps { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Player_Direction> Player_Direction { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Player_Email> Player_Email { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Player_Team> Player_Team { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PlayerPosition> PlayerPositions { get; set; }

}

}
