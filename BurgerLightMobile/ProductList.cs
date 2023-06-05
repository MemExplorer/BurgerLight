using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile
{

    public class Product
    {
        public Product(int id, string name)
        {
            PhotoId = id;
            ProductName = name;
        }

        public int PhotoId { get; set; }
        public string ProductName { get; set; }

    }

    public class ProductList
    {

        private List<Product> mProducts;

        public int ProductListCount
        {
            get { return mProducts.Count; }
        }


        public ProductList()
        {
            mProducts = new List<Product>();

            //Built it products (testing) To be removed
            Product p1 = new Product(Resource.Drawable.burger, "Burger1");
            Product p2 = new Product(Resource.Drawable.burger, "Burger2");
            Product p3 = new Product(Resource.Drawable.burger, "Burger3");
            Product p4 = new Product(Resource.Drawable.burger, "Burger4");
            Product p5 = new Product(Resource.Drawable.burger, "Burger5");
            mProducts.Add(p1);
            mProducts.Add(p2);
            mProducts.Add(p3);
            mProducts.Add(p4);
            mProducts.Add(p5);

        }

        public void AddProduct(Product p)
        {
            mProducts.Add(p);
        }

        public void SetProductList(List<Product> pl)
        {
            mProducts = pl;
        }

        // Indexer (read only) for accessing a photo:
        public Product this[int i]
        {
            get { return mProducts[i]; }
        }
    }
}