using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstModels;
using CodeFirstModels.Models;

namespace CodeFirstContext
{
   public class DataContext: DbContext
    {
        public DataContext()
            : base("name=LibraryProject")
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Admin> Admins { get; set; }

        /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
             modelBuilder.HasDefaultSchema("Admin");

             modelBuilder.Entity<Department>().ToTable("Deprtments");

             modelBuilder.Entity<Department>().HasKey<int>(d => d.ID)
 ;
         }*/
    }
}
