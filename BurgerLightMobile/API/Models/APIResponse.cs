using Android.App;
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
    internal class APIResponse
    {
        public bool success { get; set; }
        public string response { get; set; }
    }
}