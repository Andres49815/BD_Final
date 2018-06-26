using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public struct Historic_Player_Result
    {
        public Player player { get; set; }
        public IEnumerable<Player_Teams_Result> teams { get; set; }
        public IEnumerable<Player_Scores_Result> scores { get; set; }

        public Historic_Player_Result(Player p, List<Player_Teams_Result> p_t, List<Player_Scores_Result> p_s)
        {
            this.player = p;
            this.teams = p_t;
            this.scores = p_s;
        }
    }

    public struct Player_Teams_Result
    {
        [Display(Name = "Equipo")] public string team { get; set; }
        [Display(Name = "Fecha de firma")] [DataType(DataType.Date)] public DateTime signDate { get; set; }
        [Display(Name = "Fecha de separacion")] [DataType(DataType.Date)] public DateTime breakUpDate { get; set; }

        public Player_Teams_Result(string t, DateTime sD, DateTime buD)
        {
            team = t;
            signDate = sD;
            breakUpDate = buD;
        }
    }

    public struct Player_Scores_Result
    {
        [Display(Name = "Temporada")] public short startYear { get; set; }
        [Display(Name = "Promedio")] public double avarage { get; set; }

        public Player_Scores_Result(short sY, double a)
        {
            startYear = sY;
            avarage = a;
        }
    }
}
