
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using BurgerLightMobile.Fragments.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.Fragments
{
    public class OrdersFragment : AndroidX.Fragment.App.Fragment
    {

        OrderList mOrderList;
        RecyclerView mRecyclerView;
        OrderListAdapter mAdapter;
        LinearLayoutManager mLayoutManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
         
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.content_orders, container, false);

            //Populate mProductList here for RecyclerView
            //ProductList has temporary built in products
            mOrderList = new OrderList();

            // Instantiate the adapter and pass in its data source:
            mAdapter = new OrderListAdapter(mOrderList);

            // Get our RecyclerView layout:
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewOrders);

            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            mLayoutManager = new LinearLayoutManager(view.Context);
            //mLayoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false); //uncomment for gridlayout (muliple in 1 row)
            mRecyclerView.SetLayoutManager(mLayoutManager);

            return view;
        }
    }


    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (ImageView and TextView) within the CardView 
    // that is displayed in a row of the RecyclerView:
    public class OrderViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public TextView Price { get; private set; }

        public Button PlusButton { get; private set; }
        public Button MinusButton { get; private set; }
        public Button DeleteButton { get; private set; }
        public TextView QuantityTextView { get; private set; }



        public OrderViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageViewOrder);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textViewOrderName); //product name
            Price = itemView.FindViewById<TextView>(Resource.Id.textViewOrderPrice);
            

            PlusButton = itemView.FindViewById<Button>(Resource.Id.buttonAdd);
            MinusButton = itemView.FindViewById<Button>(Resource.Id.buttonMinus);

            DeleteButton = itemView.FindViewById<Button>(Resource.Id.buttonDelete);
            QuantityTextView = itemView.FindViewById<TextView>(Resource.Id.textViewQty);



            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }



    }

    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (Product List) to the RecyclerView: 

    public class OrderListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        OrderList mOrderList;

        public OrderListAdapter(OrderList productList)
        {
            mOrderList = productList;
        }

        public override int ItemCount
        {
            get { return mOrderList.ProductListCount; }
        }


        // Fill in the contents of the Product Card (invoked by the layout manager):
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OrderViewHolder vh = holder as OrderViewHolder;

           
            vh.Caption.Text = mOrderList[position].ProductName;

            //set order quantity here
            vh.QuantityTextView.Text = mOrderList[position].OrderQuantity.ToString();

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the Product List:
            try
            {
                vh.Image.SetImageResource(mOrderList[position].PhotoId);
            }
            catch
            {
                //set default photo
                vh.Image.SetImageResource(Resource.Drawable.burger);
            }


            ///////////SET OnClick of Button in Recycler View HERE
            string strProductToast = String.Format("Pressed on Product: {0}", mOrderList[position].ProductName);
            vh.PlusButton.Click += (sender, e) =>
            {
                //Should Add Quantity here
                vh.QuantityTextView.Text = (int.Parse(vh.QuantityTextView.Text) + 1).ToString(); //FOR TESTING ONLY
            };

            vh.MinusButton.Click += (sender, e) =>
            {
                //Should Minus Quantity here
                if(vh.QuantityTextView.Text != "0") vh.QuantityTextView.Text = (int.Parse(vh.QuantityTextView.Text) - 1).ToString(); //FOR TESTING ONLY
            };

            vh.DeleteButton.Click += (sender, e) =>
            {
                //Delete button here
                Toast.MakeText(Application.Context, strProductToast, ToastLength.Long).Show();
            };

        }

        // Create a new Product CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.OrdersCardView, parent, false);
            OrderViewHolder vh = new OrderViewHolder(itemView, OnClick);
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