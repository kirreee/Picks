using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Services.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class AdBasketItemViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
