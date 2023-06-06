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

namespace BurgerLightMobile.Fragments.Orders
{
    public class Order
    {
        public Order(int id, string name, int qty)
        {
            PhotoId = id;
            ProductName = name;
            OrderQuantity = qty;
        }

        public int PhotoId { get; set; }
        public string ProductName { get; set; }

        public int OrderQuantity { get; set; }

    }

    public class OrderList
    {

        private List<Order> mOrders;

        public int ProductListCount
        {
            get { return mOrders.Count; }
        }


        public OrderList()
        {
            mOrders = new List<Order>();

            //Built it products (testing) To be removed
            Order p1 = new Order(Resource.Drawable.burger, "Burger1", 0);
            Order p2 = new Order(Resource.Drawable.burger, "Burger2", 0);
            Order p3 = new Order(Resource.Drawable.burger, "Burger3", 0);
            Order p4 = new Order(Resource.Drawable.burger, "Burger4", 0);
            Order p5 = new Order(Resource.Drawable.burger, "Burger5", 0);
            mOrders.Add(p1);
            mOrders.Add(p2);
            mOrders.Add(p3);
            mOrders.Add(p4);
            mOrders.Add(p5);

        }

        public void AddProduct(Order p)
        {
            mOrders.Add(p);
        }

        public void SetProductList(List<Order> pl)
        {
            mOrders = pl;
        }

        // Indexer (read only) for accessing a photo:
        public Order this[int i]
        {
            get { return mOrders[i]; }
        }
    }
}