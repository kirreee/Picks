﻿using Picks.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Services.Interfaces
{
    public interface IImageRepository
    {
        List<Image> GetImages();
    }
}
