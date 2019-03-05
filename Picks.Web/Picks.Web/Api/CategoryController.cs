using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picks.Services.Interfaces;
using Picks.Services.ViewModels;
using Picks.Web.Models;

namespace Picks.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public ActionResult<List<Category>> GetCategories()
        {
            try
            {
                return Ok(_categoryRepository.GetCategories());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        [Route("adCategory")]
        public ActionResult <CategoryViewModel>AdCategory(AdCategoryViewModel model)
        {
            try
            {
                _categoryRepository.AdCategory(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }
    }
}