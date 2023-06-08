
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BurgerLightMobile.Activities;
using BurgerLightMobile.API;
using BurgerLightMobile.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BurgerLightMobile.Fragments
{
    public class OrdersFragment : AndroidX.Fragment.App.Fragment
    {

        List<OrderResponse> mOrderList;
        RecyclerView mRecyclerView;
        OrderListAdapter mAdapter;
        LinearLayoutManager mLayoutManager;
        Button buttonCheckout;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnStart()
        {
            base.OnStart();
            UpdateItems();
        }

        private async void UpdateItems()
        {
            await Task.Run(() => {

                this.Activity.RunOnUiThread(() => {
                    if (!BurgerLightAPI.FetchOrders(out APIResponse<List<OrderResponse>> r))
                    {
                        //Set to default values
                        Activity act = (Activity)this.Activity;
                        var TotalTextView = act.FindViewById<TextView>(Resource.Id.orderTotal);
                        var TotalTextStr = act.FindViewById<TextView>(Resource.Id.totalTxt);
                        var CartCount = act.FindViewById<TextView>(Resource.Id.CartItemCount);
                        CartCount.Text = "0";
                        TotalTextView.Text = "P 0.00";
                        TotalTextStr.Text = "TOTAL (0)";

                        mAdapter.mOrderList.Clear();
                        mAdapter.NotifyDataSetChanged();
                        return;
                    }

                    mOrderList = r.GetResponse();
                    mAdapter.mOrderList = mOrderList.ToDictionary(x => x.id);
                    mAdapter.NotifyDataSetChanged();

                });

            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
         
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.content_orders, container, false);

            //Populate mProductList here for RecyclerView
            //ProductList has temporary built in products
            mOrderList = new List<OrderResponse>();

            // Instantiate the adapter and pass in its data source:
            mAdapter = new OrderListAdapter(mOrderList, this.Context);

            // Get our RecyclerView layout:
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewOrders);

            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            mLayoutManager = new LinearLayoutManager(view.Context);
            //mLayoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false); //uncomment for gridlayout (muliple in 1 row)
            mRecyclerView.SetLayoutManager(mLayoutManager);
            buttonCheckout = view.FindViewById<Button>(Resource.Id.buttonCheckout);
            buttonCheckout.Click += ButtonCheckout_Click;
            return view;
        }

        private void ButtonCheckout_Click(object sender, EventArgs e)
        {
            if(mAdapter.mOrderList.Count == 0)
            {
                Toast.MakeText(this.Activity, "You have no items in cart!", ToastLength.Short);
                return;
            }

            Intent i = new Intent(this.Activity, typeof(CheckOutFormActivity));
            i.PutExtra("cartcount", this.Activity.Intent.GetStringExtra("cartcount"));
            i.PutExtra("username", this.Activity.Intent.GetStringExtra("username"));
            StartActivity(i);
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
        public TextView TotalTextView { get; private set; }
        public TextView CartCount { get; private set; }
        public TextView TotalTextStr { get; private set; }


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

            Activity act = (Activity)itemView.Context;
            TotalTextView = act.FindViewById<TextView>(Resource.Id.orderTotal);
            TotalTextStr = act.FindViewById<TextView>(Resource.Id.totalTxt);
            CartCount = act.FindViewById<TextView>(Resource.Id.CartItemCount);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }



    }

    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (Product List) to the RecyclerView: 

    internal class OrderUIUpdater
    {
        public OrderResponse OrderResponse { get; set; }
        public TextView QuantityTextView { get; private set; }
        public TextView TotalTextView { get; private set; }
        public TextView CartCount { get; private set; }
        public TextView TotalTextStr { get; private set; }

        public OrderUIUpdater(OrderResponse orderResponse, TextView quantityTextView, TextView totalTextView, TextView cartCount, TextView totalTextStr)
        {
            OrderResponse = orderResponse;
            QuantityTextView = quantityTextView;
            TotalTextView = totalTextView;
            CartCount = cartCount;
            TotalTextStr = totalTextStr;
        }
    }

    internal class OrderListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        public Dictionary<int, OrderResponse> mOrderList;

        Context AppCtx;
        public OrderListAdapter(List<OrderResponse> productList, Context appCtx)
        {
            mOrderList = productList.ToDictionary(x => x.id);
            AppCtx = appCtx;
        }

        public override int ItemCount
        {
            get { return mOrderList.Count; }
        }


        // Fill in the contents of the Product Card (invoked by the layout manager):
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OrderViewHolder vh = holder as OrderViewHolder;

            OrderResponse currOrderResp = mOrderList.ElementAt(position).Value;
            OrderUIUpdater u = new OrderUIUpdater(currOrderResp, vh.QuantityTextView, vh.TotalTextView, vh.CartCount, vh.TotalTextStr);
            vh.Caption.Text = currOrderResp.name;
            vh.Price.Text = "P " + currOrderResp.price.ToString("n2");
            //set order quantity here
            vh.QuantityTextView.Text = currOrderResp.quantity.ToString();
            UpdateOrderUI(u);
            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the Product List:
            try
            {
                vh.Image.SetImageResource(Resource.Drawable.burger);
            }
            catch
            {
                //set default photo
                vh.Image.SetImageResource(Resource.Drawable.burger);
            }

            ///////////SET OnClick of Button in Recycler View HERE
            vh.PlusButton.Click += (s, e) => PlusButton_Click(s, e, u);
            vh.MinusButton.Click += (s, e) => MinusButton_Click(s, e, u);
            vh.DeleteButton.Click += (s, e) => DeleteButton_Click(s, e, u);

            DestroySubscribers(vh.PlusButton);
            DestroySubscribers(vh.MinusButton);
            DestroySubscribers(vh.DeleteButton);
        }

        //hack to destroy duplicate delegate subscribers. Made by yours truly :)
        private void DestroySubscribers(Button b)
        {
            Android.Views.View v = b;
            var field = typeof(View).GetField("weak_implementor_SetOnClickListener", BindingFlags.NonPublic | BindingFlags.Instance);
            Type internalOnclickImpl = typeof(Android.Views.View).GetNestedType("IOnClickListenerImplementor", BindingFlags.NonPublic);
            var onClickImplHandlerField = internalOnclickImpl.GetField("Handler", BindingFlags.Public | BindingFlags.Instance);
            var refInstance = (System.WeakReference)field.GetValue(v);
            if (refInstance == null)
            {
                return;
            }

            var OnClickImpl = refInstance.Target;
            var multicastDelInfo = (MulticastDelegate)onClickImplHandlerField.GetValue(OnClickImpl);
            var delegateListField = typeof(MulticastDelegate).GetField("delegates", BindingFlags.NonPublic | BindingFlags.Instance);

            //get rid of all subscribed events
            var handlerList = multicastDelInfo.GetInvocationList();
            if (handlerList.Length > 1)
                delegateListField.SetValue(multicastDelInfo, new Delegate[] {handlerList.Last()});
        }

        private void DeleteButton_Click(object sender, EventArgs e, OrderUIUpdater u)
        {
            APIUpdateCart(-u.OrderResponse.quantity, u);
        }

        private void MinusButton_Click(object sender, EventArgs e, OrderUIUpdater u)
        {
            APIUpdateCart(-1, u);
        }

        private void PlusButton_Click(object sender, EventArgs e, OrderUIUpdater u)
        {
            APIUpdateCart(1, u);
        }

        private void APIUpdateCart(int val, OrderUIUpdater u)
        {
            if(!BurgerLightAPI.AddCart(u.OrderResponse.id, val, out APIResponse<AddCartResponse> r))
            {
                Toast.MakeText(this.AppCtx, r.GetMessage(), ToastLength.Short).Show();
                return;
            }

            AddCartResponse resp = r.GetResponse();
            u.QuantityTextView.Text = resp.newvalue.ToString();
            mOrderList[u.OrderResponse.id].quantity = resp.newvalue;
            UpdateOrderUI(u);

            if (resp.newvalue == 0)
            {
                mOrderList.Remove(u.OrderResponse.id);
                this.NotifyDataSetChanged();
            }

        }

        private void UpdateOrderUI(OrderUIUpdater u)
        {
            u.TotalTextView.Text = "P " + mOrderList.Sum(x => x.Value.price * x.Value.quantity).ToString("n2");

            string totalQ = mOrderList.Sum(x => x.Value.quantity).ToString();
            u.CartCount.Text = totalQ;
            u.TotalTextStr.Text = "TOTAL (" + totalQ + ")";
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