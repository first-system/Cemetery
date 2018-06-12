using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class BurialPlace
    {
        public int Id { get; set; }

        public int NArea { get; set; }

        public int NBurial { get; set; }

        public List<Deceased> Deceaseds { get; set; }
    }
}