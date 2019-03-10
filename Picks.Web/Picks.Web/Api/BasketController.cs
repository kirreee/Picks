using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picks.DAL.DataAccess;
using Picks.DAL.Models;
using Picks.Services.ViewModels;

namespace Picks.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly DefaultDataContext _ctx;

        public BasketController(DefaultDataContext context)
        {
            _ctx = context;
        }

        [Route("getBasketList")]
        [HttpGet]
        public IActionResult GetBasketList()
        {
            var viewModelList = new List<BasketViewModel>();
            var basketItems = _ctx.Basket.ToList();
            foreach(var item in basketItems)
            {
                var vm = new BasketViewModel();
                {
                    vm.Id = item.Id;
                    vm.FileName = item.FileName;
                    vm.ImageUrl = item.ImageUrl;
                }

                viewModelList.Add(vm);
            }

            return Ok(viewModelList);
        }

        [Route("adBasketItem")]
        [HttpPost]
        public IActionResult AdToBasket(AdBasketItemViewModel vm)
        {
            var model = new Basket();
            {
                model.FileName = vm.FileName;
            }

            try
            {
                _ctx.Basket.Add(model);
                _ctx.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("removeBasketItem/{id}")]
        [HttpPost]
        public IActionResult RemoveItemFromBasket(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Basket basketItem = _ctx.Basket
                .FirstOrDefault(x => x.Id == id);
            try
            {
                _ctx.Basket.Remove(basketItem);
                _ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}