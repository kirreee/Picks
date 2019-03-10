using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Services.ViewModels
{
    public class ImageVieModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }

    public class AdImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }

    public class SaveImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }

    public class DownloadImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }

    public class GetImagesByCategoryViewModel
    {
        public string CategoryName { get; set; }
    }
}
