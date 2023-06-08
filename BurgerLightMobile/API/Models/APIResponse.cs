using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.API.Models
{
    internal class APIResponse<ResType>
    {
        public bool success { get; set; }
        public string response { get; set; }

        public ResType GetResponse()
        {
            if(success)
                return JsonConvert.DeserializeObject<ResType>(response);

            return default(ResType);
        }

        public string GetError()
        {
            if(!success)
                return response;

            return "";
        }
    }
}