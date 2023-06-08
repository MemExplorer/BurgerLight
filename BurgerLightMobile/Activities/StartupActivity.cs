using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "StartupActivity", MainLauncher = true)]
    public class StartupActivity : Activity
    {
        private Button registerBtn;
        private Button loginBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.startup_layout);

            registerBtn = FindViewById<Button>(Resource.Id.createaccBtn);
            registerBtn.Click += RegisterBtn_Click;

            loginBtn = FindViewById<Button>(Resource.Id.loginbutton);
            loginBtn.Click += LoginBtn_Click;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Intent t = new Intent(this, typeof(LoginActivity));
            StartActivity(t);
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Intent t = new Intent(this, typeof(RegisterActivity));
            StartActivity(t);
        }
    }
}