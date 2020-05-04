using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data
{
   public class APIDataContext: DbContext
    {
        public APIDataContext(DbContextOptions<APIDataContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        
    }
}
