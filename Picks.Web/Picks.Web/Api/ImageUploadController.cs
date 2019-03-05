using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picks.DAL.DataAccess;
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

        [HttpPost]
        [Route("adImage")]
        public IActionResult AdFile(List<IFormFile> files)
        {

            IList<string> allowedFileExtension = new List<string> { ".jpg", "jpeg", "png" };
            

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

        [HttpPost]
        [Route("SaveImage")]
        public IActionResult SaveImage(SaveImageViewModel model)
        {

            Image image = _ctx.Images
                .FirstOrDefault(x => x.Id == model.Id);

            image.FileName = model.FileName;
            image.ImageUrl = model.ImageUrl;
            image.CategoryId = model.CategoryId;
            image.Category = _ctx.Categories
                .FirstOrDefault(x => x.Id == model.CategoryId);

            var vm = new ImageVieModel();
            {
                vm.CategoryName = image.Category.Name;
                vm.FileName = image.FileName;
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

        [HttpGet]
        [Route("getAllImages")]
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
    }
}