using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.Web.Models
{
    public class Image
    {
        //Image
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }

        //Categories
        public virtual Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
