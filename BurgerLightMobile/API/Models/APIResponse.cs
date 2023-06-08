using Newtonsoft.Json.Linq;

namespace BurgerLightMobile.API.Models
{
    internal class APIResponse<ResType>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public JContainer data { get; set; }

        internal ResType GetResponse()
            => data.ToObject<ResType>();

        internal string GetMessage() 
            => message;
    }
}