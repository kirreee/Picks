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
    public class ImageRepository : IImageRepository
    {
        private readonly DefaultDataContext _ctx;

        public ImageRepository(DefaultDataContext context)
        {
            _ctx = context;
        }

        public List<ImageVieModel> GetImages()
        {
            var ListOfImages = new List<ImageVieModel>();
            var images = _ctx.Images.ToList();
            images.ForEach(img =>
            {
                var categories = _ctx.Categories.Where(x => x.Id == img.CategoryId);
                var catName = "";
                foreach (var cats in categories)
                {
                    catName = cats.Name;
                }

                var imageViewModel = new ImageVieModel()
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    FileName = img.FileName,
                    CategoryName = catName
                };

                ListOfImages.Add(imageViewModel);
            });

            return ListOfImages;
        }
    }
}
