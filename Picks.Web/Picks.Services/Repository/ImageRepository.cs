using Picks.DAL.DataAccess;
using Picks.Services.Interfaces;
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

        public List<Image> GetImages()
        {
            return _ctx.Images.ToList();
        }
    }
}
