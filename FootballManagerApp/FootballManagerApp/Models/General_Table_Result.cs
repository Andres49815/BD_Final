using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public struct General_Table_Result
    {
        [Display(Name = "Equipo")] public string team { get; set; }
        [Display(Name = "Partidos Jugados")] public int played { get; set; }
        [Display(Name = "Ganados")] public int won { get; set; }
        [Display(Name = "Perdidos")] public int lost { get; set; }
        [Display(Name = "Empatados")] public int drawn { get; set; }
        [Display(Name = "G. a favor")] public int goalsFavor { get; set; }
        [Display(Name = "G. en contra")] public int goalsAgainst { get; set; }
        [Display(Name = "Puntos")] public int points { get; set; }
        [Display(Name = "Posicion")] public int possition { get; set; }

        public General_Table_Result(string t, int w, int l, int d, int gF, int gA, int p, int i)
        {
            team = t;
            won = w;
            lost = l;
            drawn = d;
            goalsFavor = gF;
            goalsAgainst = gA;
            points = p;
            played = w + d + l;
            possition = i;
        }
    }
}