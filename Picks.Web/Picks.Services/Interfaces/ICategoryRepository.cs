using Picks.Services.Repository;
using Picks.Services.ViewModels;
using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picks.Services.Interfaces
{
    public interface ICategoryRepository 
    {
        List<Category> GetCategories();
        void AdCategory(AdCategoryViewModel category);
    }
}
