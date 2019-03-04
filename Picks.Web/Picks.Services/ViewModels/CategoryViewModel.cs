using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Services.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public List<Image> Images { get; set; }
    }

    public class AdCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
