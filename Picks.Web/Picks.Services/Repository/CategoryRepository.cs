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
            //var imageList = new List<Image>();
            var vm = new List<CategoryViewModel>();

            var categories = _ctx.Categories.ToList();
            //var images = _ctx.Images.ToList();

            //foreach (var imgs in images)
            //{
            //   imageList.Add(_ctx.Images.FirstOrDefault(x => x.Id == imgs.Id));
            //}

            foreach (var item in categories)
            {
                var viewModel = new CategoryViewModel();
                {
                    viewModel.Id = item.Id;
                    viewModel.Name = item.Name;
                    //viewModel.Images = imageList;
                    
                }

                vm.Add(viewModel);
            }

            return vm;
        }
    }
}
