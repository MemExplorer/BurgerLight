using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "Checkout Completed", Theme = "@style/LoginTheme")]
    public class CheckOutCompleteActivity : AppCompatActivity
    {
        Button btnReturn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.checkout_complete_layout);
            // Create your application here
            btnReturn = FindViewById<Button>(Resource.Id.buttonReturn);
            btnReturn.Click += btnReturn_Click;

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            //Back to Main
            Intent t = new Intent(this, typeof(MainActivity));
            t.PutExtra("cartcount", "0");
            t.PutExtra("username", this.Intent.GetStringExtra("username"));
            StartActivity(t);
            Finish();

        }
    }
}