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
    [Activity(Label = "LoginActivity", Theme = "@style/LoginTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {

        private Button loginBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_layout);

            loginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            loginBtn.Click += LoginBtn_Click;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Intent t = new Intent(this, typeof(MainActivity));
            StartActivity(t);
        }
    }
}