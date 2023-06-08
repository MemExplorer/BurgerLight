using Newtonsoft.Json.Linq;

namespace BurgerLightMobile.API.Models
{
    internal class APIResponse<ResType>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        internal ResType GetResponse()
        {
            if (data is JContainer c)
                return c.ToObject<ResType>();
            else
                return (ResType)data;
        }

        internal string GetMessage() 
            => message;
    }
}