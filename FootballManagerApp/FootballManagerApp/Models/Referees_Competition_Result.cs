using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballManagerApp.Models
{
    public class Referees_Competition_Result
    {
        [Display(Name = "Arbitro")] public string refereeName { get; set; }
        [Display(Name = "Promedio de Calificaicones")] public double avarage { get; set; }

        // CONSTRUCTORS
        public Referees_Competition_Result()
        {
            refereeName = "";
            avarage = 0.0;
        }
        public Referees_Competition_Result(string rN, double a)
        {
            this.refereeName = rN;
            this.avarage = a;
        }
    }
}