using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

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

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            UpdateMainActivity();
        }

        private void UpdateMainActivity()
        {
            MainActivity.Instance.SetTab(Resource.Id.nav_home);
            MainActivity.Instance.CartCount.Text = "0";
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Finish();
            UpdateMainActivity();
        }
    }
}