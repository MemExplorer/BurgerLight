
using BurgerLightMobile.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BurgerLightMobile.API
{
    internal class BurgerLightAPI
    {
        const string domain = "http://172.23.112.1";
        private static CookieContainer cookieContainer = new CookieContainer();
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
                    return JsonConvert.DeserializeObject<APIResponse<RetType>>(responsestr);
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool LoginUser(string username, string password, out APIResponse<LoginResponse> Response)
        {
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "usrnm", username },
                { "pswd", password }
            };

            Response = CreateAPIRequest<LoginResponse>("/controllers/user/userlogin.php", paramlist);

            return Response != null && Response.success;
        }

        public static bool RegisterUser(string username, string password, out APIResponse<string> Response)
        {
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "usrnm", username },
                { "pswd", password }
            };

            Response = CreateAPIRequest<string>("/controllers/user/userregister.php", paramlist);

            return Response != null && Response.success;
        }

        public static bool LogoutUser(out APIResponse<string> Response)
        {
            Response = CreateAPIRequest<string>("/controllers/user/userlogout.php");

            return Response != null && Response.success;

        }

        public static bool FetchMenu(out APIResponse<List<MenuResponse>> Response)
        {
            Response = CreateAPIRequest<List<MenuResponse>>("/controllers/GetMenu.php");

            return Response != null && Response.success;
        }

        public static bool AddCart(int id, int q, out APIResponse<AddCartResponse> Response)
        {
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                { "q", q.ToString() }
            };

            Response = CreateAPIRequest<AddCartResponse>("/controllers/AddCart.php", paramlist);

            return Response != null && Response.success;
        }

        public static bool FetchOrders(out APIResponse<List<OrderResponse>> Response)
        {
            Response = CreateAPIRequest<List<OrderResponse>>("/controllers/FetchOrders.php");

            return Response != null && Response.success;
        }

        public static bool ProcessOrder(string[] userInfo, out APIResponse<string> Response)
        {
            Dictionary<string, string> paramlist = new Dictionary<string, string>
            {
                { "lname", userInfo[0] },
                { "fname", userInfo[1] },
                { "email", userInfo[2] },
                { "tel", userInfo[3] },

                { "street", userInfo[4] },
                { "city", userInfo[5]},
                { "province", userInfo[6] },
                { "zip", userInfo[7]},

            };

            Response = CreateAPIRequest<string>("/controllers/ProcessOrder.php", paramlist);


            return Response != null && Response.success;
        }
    }
}