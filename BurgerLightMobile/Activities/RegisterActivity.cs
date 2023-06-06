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
    [Activity(Label = "Registration", Theme = "@style/LoginTheme")]
    public class RegisterActivity : AppCompatActivity
    {
        private Button registerBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register_layout);

            registerBtn = FindViewById<Button>(Resource.Id.RegisterBtn);
            registerBtn.Click += RegisterBtn_Click;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            //TODO: Process information to server
            var intent = new Intent(this, typeof(LoginActivity));

            //make sure we can't go back to register form
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
            StartActivity(intent);
        }
    }
}