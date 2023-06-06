using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static Java.Util.Jar.Attributes;

namespace BurgerLightMobile.API
{
    internal class BurgerLightAPI
    {
        const string domain = "http://192.168.254.203/";

        private static string CreateAPIRequest(string path, Dictionary<string, string> paramList = null)
        {
            string url = domain + path;

            //add arguments
            if(paramList != null && paramList.Count > 0)
                url += "?" + string.Join("&", paramList.Select(x => x.Key + "=" + x.Value));

            //create web request
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                return reader.ReadToEnd();
        }
    }
}