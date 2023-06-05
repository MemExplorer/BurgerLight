using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile
{
    public class ProductMenu : AndroidX.Fragment.App.Fragment
    {
        ProductList mProductList;
        RecyclerView mRecyclerView;
        ProductListAdapter mAdapter;
        LinearLayoutManager mLayoutManager;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ProductMenuLayout, container, false);

            // Create your fragment here
            mProductList = new ProductList();

            // Instantiate the adapter and pass in its data source:
            mAdapter = new ProductListAdapter(mProductList);

            // Get our RecyclerView layout:
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            mLayoutManager = new LinearLayoutManager(view.Context);
            //mLayoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            return view;

        }
    }

    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (ImageView and TextView) within the CardView 
    // that is displayed in a row of the RecyclerView:
    public class ProductViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }

        public TextView Caption { get; private set; }

        public Button AddButton { get; private set; }

        public ProductViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);

            AddButton = ItemView.FindViewById<Button>(Resource.Id.button1);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }



    }

    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (Product List) to the RecyclerView: 

    public class ProductListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        ProductList mProductList;

        public ProductListAdapter(ProductList productList)
        {
            mProductList = productList;
        }

        public override int ItemCount
        {
            get { return mProductList.ProductListCount; }
        }


        // Fill in the contents of the Product Card (invoked by the layout manager):
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ProductViewHolder vh = holder as ProductViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the Product List:
            vh.Image.SetImageResource(mProductList[position].PhotoId);
            vh.Caption.Text = mProductList[position].ProductName;


            ///////////SET OnClick of Button in Recycler View HERE
            string strProductToast = String.Format("Pressed on Product: {0}", mProductList[position].ProductName);
            vh.AddButton.Click += (sender, e) =>
            {
                Toast.MakeText(Application.Context, strProductToast, ToastLength.Long).Show();

            };


        }

        // Create a new Product CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ProductCardView, parent, false);
            ProductViewHolder vh = new ProductViewHolder(itemView, OnClick);
            return vh;
        }


        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}