using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BurgerLightMobile.API.Models;
using Java.Util;
using Newtonsoft.Json;
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

        public static bool LoginUser(string username, string password, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            string strApiResp = CreateAPIRequest("/controllers/user/userlogin.php", paramlist);
            APIResponse apiresponse = JsonConvert.DeserializeObject<APIResponse>(strApiResp);

            //set error message if not success
            if (!apiresponse.success)
                ErrorMessage = apiresponse.response;

            return apiresponse.success;
        }

        public static bool RegisterUser(string username, string password, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            string strApiResp = CreateAPIRequest("/controllers/user/useregister.php", paramlist);
            APIResponse apiresponse = JsonConvert.DeserializeObject<APIResponse>(strApiResp);

            //set error message if not success
            if (!apiresponse.success)
                ErrorMessage = apiresponse.response;

            return apiresponse.success;
        }
    }
}