using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public struct Historic_Coach_Result
    {
        public Coach coach { get; set; }
        public IEnumerable<Coach_Teams_Result> teams { get; set; }

        public Historic_Coach_Result(Coach c, IEnumerable<Coach_Teams_Result> t)
        {
            this.coach = c;
            this.teams = t;
        }
    }

    public struct Coach_Teams_Result
    {
        [Display(Name = "Equipo")] public string team { get; set; }
        [Display(Name = "Fecha de Firma")] [DataType(DataType.Date)] public DateTime signDate { get; set; }
        [Display(Name = "Fecha de separacion")] [DataType(DataType.Date)] public DateTime expirationDate { get; set; }

        public Coach_Teams_Result(string t, DateTime s, DateTime e)
        {
            this.team = t;
            this.signDate = s;
            this.expirationDate = e;
        }
    }
}