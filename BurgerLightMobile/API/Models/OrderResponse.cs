

namespace BurgerLightMobile.API.Models
{
    internal class OrderResponse
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public float price { get; set; }
    }
}