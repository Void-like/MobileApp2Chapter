using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string ThrityName { get; set; }

         public DateTime BirthDay { get; set; }
        public double Ocenochka { get; set; }
        public string Gender {  get; set; }
        public bool IsAlive { get; set; }


    }
}
