using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BurgerLightMobile.API.Models;
using Java.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        const string domain = "http://192.168.254.207";
        private static CookieContainer cookieContainer = new CookieContainer();
        private static APIResponse<RetType> GetResponse<RetType>(string strResponse)
        {
            JObject obj = (JObject)JsonConvert.DeserializeObject(strResponse);
            APIResponse<RetType> i = new APIResponse<RetType>();
            i.success = (bool)obj["success"];
            i.response = obj["response"].ToString();
            return i;
        }

        private static APIResponse<RetType> CreateAPIRequest<RetType>(string path, Dictionary<string, string> paramList = null)
        {
            string url = domain + path;

            //add arguments
            if(paramList != null && paramList.Count > 0)
                url += "?" + string.Join("&", paramList.Select(x => x.Key + "=" + x.Value));

            //create web request
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                var response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responsestr = reader.ReadToEnd();
                    return GetResponse<RetType>(responsestr);
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool LoginUser(string username, string password, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "usrnm", username },
                { "pswd", password }
            };

            APIResponse<LoginResponse> apiResp = CreateAPIRequest<LoginResponse>("/controllers/user/userlogin.php", paramlist);

            if (!apiResp.success)
                ErrorMessage = apiResp.GetError();

            return apiResp.success;
        }

        public static bool RegisterUser(string username, string password, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "usrnm", username },
                { "pswd", password }
            };

            APIResponse<string> apiResp = CreateAPIRequest<string>("/controllers/user/userregister.php", paramlist);

            if (!apiResp.success)
                ErrorMessage = apiResp.GetError();

            return apiResp.success;
        }

        public static bool LogoutUser(out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            APIResponse<string> apiResp = CreateAPIRequest<string>("/controllers/user/userlogout.php");

            if (!apiResp.success)
                ErrorMessage = apiResp.GetError();

            return apiResp.success;

        }
    }
}