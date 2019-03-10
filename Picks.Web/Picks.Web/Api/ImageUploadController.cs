using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Picks.DAL.DataAccess;
using Picks.DAL.Models;
using Picks.Services.Interfaces;
using Picks.Services.ViewModels;
using Picks.Web.Models;

namespace Picks.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {

        private readonly DefaultDataContext _ctx;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IImageRepository _repo;

        public ImageUploadController(DefaultDataContext context,
            IHostingEnvironment hostingEnvironment, IImageRepository repository)
        {
            _ctx = context;
            _hostingEnvironment = hostingEnvironment;
            _repo = repository;
        }

        [Route("adImage")]
        [HttpPost]
        public IActionResult AdFile(List<IFormFile> files)
        {

            if (files.Count() == 0)
            {
                return NotFound();
            }

            IList<string> allowedFileExtension = new List<string> { ".jpg", ".jpeg", ".png" };
            var fileName = "";

            foreach (var item in files)
            {
                fileName = item.FileName;
            }

            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();

            if (!allowedFileExtension.Contains(extension))
            {
                return BadRequest();
            }

            // full path to file in temp location
            var filePath = _hostingEnvironment.WebRootPath + "\\ImageUpload\\" + fileName;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyToAsync(stream);
                    }
                }
            }

            var image = new Image();
            {
                image.ImageUrl = fileName;
                image.FileName = fileName;
            }

            try
            {
                _ctx.Images.Add(image);
                _ctx.SaveChanges();

                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("SaveImage")]
        [HttpPost]
        public IActionResult SaveImage(SaveImageViewModel model)
        {

            Image image = _ctx.Images
                .FirstOrDefault(x => x.Id == model.Id);

            image.Category = _ctx.Categories
               .FirstOrDefault(x => x.Id == model.CategoryId);

            image.FileName = model.FileName;
            image.ImageUrl = model.ImageUrl;
            image.CategoryId = model.CategoryId;

            var vm = new ImageVieModel();
            {
                vm.CategoryName = image.Category.Name;
                vm.FileName = image.FileName + new Guid();
                vm.ImageUrl = image.ImageUrl;
            }

            try
            {
                _ctx.Entry(image).State = EntityState.Modified;
                _ctx.SaveChanges();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("getAllImages")]
        [HttpGet]
        public IActionResult GetImages()
        {
            try
            {
                return Ok(_repo.GetImages());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [Route("downloadImage")]
        [HttpPost]
        public IActionResult DownloadImage(DownloadImageViewModel model)
        {
            var fileName = model.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var guidId = Guid.NewGuid();
            string zipName = "Images" + guidId + ".zip";
            string rootPath = _hostingEnvironment.WebRootPath + "/ImageUpload/";
            string zipPath = _hostingEnvironment.WebRootPath + "/ImagesZipFiles/";
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(zipPath))
            {
                return NotFound("Root Path not found!");
            }

            var zip = ZipFile.Open(zipPath + zipName, ZipArchiveMode.Create);

            try
            {
                zip.CreateEntryFromFile(rootPath + model.FileName, Path.GetFileName(rootPath + model.FileName), CompressionLevel.Optimal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            zip.Dispose();
            return Ok(zipName);
        }

        [Route("downloadImagesFromBasket")]
        [HttpPost]
        public IActionResult DownloadAllImagesFromBasket(IEnumerable<DownloadImageViewModel> model)
        {

            if(model.Count() <= 0)
            {
                return NotFound();
            }

            foreach (var basketItem in model)
            {
                Basket basket = _ctx.Basket.FirstOrDefault(x => x.Id == basketItem.Id);
                _ctx.Basket.Remove(basket);
                _ctx.SaveChanges();
            }
  

            var guidId = Guid.NewGuid();
            string zipName = "Images" + guidId + ".zip";
            string rootPath = _hostingEnvironment.WebRootPath + "/ImageUpload/";
            string zipPath = _hostingEnvironment.WebRootPath + "/ImagesZipFiles/";
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(zipPath))
            {
                return NotFound("Root Path not found!");
            }

            var zip = ZipFile.Open(zipPath + zipName, ZipArchiveMode.Create);

            try
            {
                foreach (var files in model)
                {
                    zip.CreateEntryFromFile(rootPath + files.FileName, Path.GetFileName(rootPath + files.FileName), CompressionLevel.Optimal);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            zip.Dispose();
            return Ok(zipName);
        }

        [Route("filteringImage")]
        [HttpPost]
        public IActionResult GetImagesByCategories(GetImagesByCategoryViewModel model)
        {
            if (model.CategoryName == "Show all")
            {
                return Ok(_ctx.Images.ToList());
            }

            var images = _ctx.Images
                .Where(x => x.Category.Name == model.CategoryName);

            return Ok(images);
        }
    }
}