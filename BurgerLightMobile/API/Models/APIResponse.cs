using Newtonsoft.Json;

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