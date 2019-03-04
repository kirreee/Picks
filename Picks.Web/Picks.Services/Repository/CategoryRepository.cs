using Picks.DAL.DataAccess;
using Picks.Services.Interfaces;
using Picks.Services.ViewModels;
using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picks.Services.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DefaultDataContext _ctx;
        
        public CategoryRepository(DefaultDataContext ctx)
        {
            _ctx = ctx;
        }

        public void AdCategory(AdCategoryViewModel category)
        {
            var data = new Category();
            {
                data.Name = category.Name;
            }

            _ctx.Add(data);
            _ctx.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _ctx.Categories.ToList();
        }
    }
}
