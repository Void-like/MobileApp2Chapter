using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class MoviesAuthors 
    {
        public int Id { get; set; } 
        public int IdAuthor { get; set; }
        public int IdMovie { get; set; }

        public Author Author { get; set; }
        public Movie Movie { get; set; }


    }
}
