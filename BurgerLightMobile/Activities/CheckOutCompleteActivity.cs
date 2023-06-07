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

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "CheckOutCompleteActivity")]
    public class CheckOutCompleteActivity : Activity
    {
        Button btnReturn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            btnReturn = FindViewById<Button>(Resource.Id.buttonReturn);
            btnReturn.Click += btnReturn_Click;

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            //Back to Main
            Intent t = new Intent(this, typeof(MainActivity));
            StartActivity(t);
            Finish();

        }
    }
}