using Microsoft.EntityFrameworkCore;
using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.DAL.DataAccess
{
    public class DefaultDataContext : DbContext
    {
        public DefaultDataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category>Categories { get; set; }
        public DbSet<Image>Images { get; set; }


    }
}
