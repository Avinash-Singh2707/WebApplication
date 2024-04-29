﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CodeFirstApproach.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext() : base("connectstr")
        { }
        public DbSet<Movie> movies { get; set; }
    }
}