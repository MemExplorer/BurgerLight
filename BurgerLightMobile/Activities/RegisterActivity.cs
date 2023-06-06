using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "Registration", Theme = "@style/LoginTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        private Button registerBtn;
        private Button backBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register_layout);

            registerBtn = FindViewById<Button>(Resource.Id.RegisterBtn);
            registerBtn.Click += RegisterBtn_Click;

            backBtn = FindViewById<Button>(Resource.Id.BackBtnRegister);
            backBtn.Click += BackBtn_Click;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            //TODO: Process information to server
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
        }
    }
}