using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Movie
    {
       public int Id { get; set; }  
       public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Ocenochka { get; set; }

        public string Genre { get; set; }

        public double Minutes { get; set; }
        public Image Image { get; set; }



    }
}
