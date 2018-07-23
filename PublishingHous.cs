using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CFProject.Model;

namespace CFProject
{
    class PublishingHous : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public PublishingHous()
            :base("name=DefaultConnection")
        { }
    }
}
