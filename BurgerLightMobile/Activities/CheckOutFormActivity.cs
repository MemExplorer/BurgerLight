using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using BurgerLightMobile.API;
using System;

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "Checkout Form", Theme = "@style/LoginTheme")]
    public class CheckOutFormActivity : AppCompatActivity
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
            EditText[] arrEt = new EditText[] { etLname, etFname, etEmail, etPhoneNum, etStreet, etCity, etProvince, etZip };
            string[] errorMsg = new string[] { "Last Name", "First Name", "Email", "Phone Number", "Street Input", "City Input", "Province Input", "Zip Code" };
            bool errFlag = false;
            for(int z = 0 ; z < arrEt.Length; z++)
            {
                arrEt[z].SetError((string)null, null);
                if (arrEt[z].Text.Length == 0)
                {
                    errFlag = true;
                    arrEt[z].Error = "Invalid " + errorMsg[z] + "!";
                }
            }

            if (errFlag)
                return;
            //Proceed to checkout completed activity
            if(!BurgerLightAPI.ProcessOrder(out string eMsg))
            {
                Toast.MakeText(this, eMsg, ToastLength.Short).Show(); 
                return;
            }

            Intent i = new Intent(this, typeof(CheckOutCompleteActivity));
            i.PutExtra("cartcount", this.Intent.GetStringExtra("cartcount"));
            i.PutExtra("username", this.Intent.GetStringExtra("username"));
            StartActivity(i);
            Finish();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Back to Main
            Finish();

        }

    }
}