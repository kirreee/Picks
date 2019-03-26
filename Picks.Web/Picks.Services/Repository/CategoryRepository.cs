using Picks.DAL.DataAccess;
using Picks.Services.Interfaces;
using Picks.Services.ViewModels;
using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public List<CategoryViewModel> GetCategories()
        {

            var categoriesList = new List<CategoryViewModel>();
            //var imageList = new List<Image>();
            var categories = _ctx.Categories.ToList();
            categories.ForEach(category =>
            {
                //var images = _ctx.Images
                //    .Where(x => x.CategoryId == category.Id)
                //    .ToList();

                //imageList.AddRange(images);

                var categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                    //Images = imageList
                };

                categoriesList.Add(categoryViewModel);
            });

            return categoriesList;
        }
    }
}
