using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproach.Models
{
    public class Movie
    {
        [Key]
        public int mid { get; set; }
        public string Moviename { get; set; }
        public string DateofRelease { get; set; }


    }
}