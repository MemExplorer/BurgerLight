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
    [Activity(Label = "CheckOutFormActivity")]
    public class CheckOutFormActivity : Activity
    {

        Button btnCheckout, btnBack;
        EditText etLname, etFname, etEmail, etPhoneNum, etStreet, etCity, etProvince, etZip;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.checkout_form_layout);

            //Get Session Here

            // Create your application here

            btnCheckout = FindViewById<Button>(Resource.Id.buttonCheckout);
            btnBack = FindViewById<Button>(Resource.Id.buttonBack);

            etLname = FindViewById<EditText>(Resource.Id.editTextLname);
            etFname = FindViewById<EditText>(Resource.Id.editTextFname);
            etEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            etPhoneNum = FindViewById<EditText>(Resource.Id.editTextPhoneNum);

            etStreet = FindViewById<EditText>(Resource.Id.editTextStreet);
            etCity = FindViewById<EditText>(Resource.Id.editTextCity);
            etProvince = FindViewById<EditText>(Resource.Id.editTextProvince);
            etZip = FindViewById<EditText>(Resource.Id.editTextZip);


            btnBack.Click += btnBack_Click;
            btnCheckout.Click += btnCheckout_Click;



        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            //Proceed to checkout completed activity
            


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Back to Main
            Intent t = new Intent(this, typeof(MainActivity));
            StartActivity(t);

        }

    }
}