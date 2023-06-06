﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.API.Models
{
    internal class MenuResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public float price { get; set; }
    }
}