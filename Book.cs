using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFProject.Model
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Autor { get; set; }
        public decimal Price { get; set; }
        public Publisher Publisher { get; set; }
        public int PublisherID { get; set; }
        public int GenreID { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }


    }
}
