using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public struct HistoricMatchesResult
    {
        public IEnumerable<MatchesResult> matches { get; set; }
        public IEnumerable<TopScorersResult> scorers { get; set; }

        public HistoricMatchesResult(IEnumerable<MatchesResult> m, IEnumerable<TopScorersResult> t)
        {
            this.matches = m;
            this.scorers = t;
        }
    }

    public struct MatchesResult
    {
        [Display(Name = "Casa")] public string home { get; set; }
        [Display(Name = "Visita")] public string away { get; set; }
        [Display(Name = "g. casa")] public int goalsHome { get; set; }
        [Display(Name = "g. visita")] public int goalsAway { get; set; }
        [Display(Name = "jornada")] public int matchday { get; set; }
        [Display(Name = "temoprada")] public int year { get; set; }

        public MatchesResult(string h, string a, int gH, int gA, int m, int y)
        {
            this.home = h;
            this.away = a;
            this.goalsHome = gH;
            this.goalsAway = gA;
            this.matchday = m;
            this.year = y;
        }
    }

    public struct TopScorersResult
    {
        [Display(Name = "Nombre")] public string name { get; set; }
        [Display(Name = "Goles")] public int totalGoals { get; set; }

        public TopScorersResult(string n, int t)
        {
            this.name = n;
            this.totalGoals = t;
        }
    }
}