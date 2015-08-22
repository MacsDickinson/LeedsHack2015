using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundJobs.MVC.Models
{
    public class SurveyIndexViewModel
    {
        public string Postcode { get; set; }
        public string WorkLocation { get; set; }
        public string FavoriteCoffee { get; set; }
        public string FavoriteBeer { get; set; }
        public TattooLevel Tattoos { get; set; }
        public string ArtistsDiscovered { get; set; }
        public string Diet { get; set; }
        public string TakeOut { get; set; }
    }

    public enum TattooLevel
    {
        Some = 1,
        HalfSleeve = 2,
        FullSleeve = 3,
        Covered = 4
    }
}
