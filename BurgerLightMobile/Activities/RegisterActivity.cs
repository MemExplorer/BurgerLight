using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using BurgerLightMobile.API;
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
        private EditText usernameTxt;
        private EditText passwordTxt;
        private EditText passwordConfirmTxt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register_layout);

            registerBtn = FindViewById<Button>(Resource.Id.RegisterBtn);
            registerBtn.Click += RegisterBtn_Click;

            backBtn = FindViewById<Button>(Resource.Id.BackBtnRegister);
            backBtn.Click += BackBtn_Click;

            usernameTxt = FindViewById<EditText>(Resource.Id.registerUsername);
            passwordTxt = FindViewById<EditText>(Resource.Id.registerPassword);
            passwordConfirmTxt = FindViewById<EditText>(Resource.Id.registerConfirmPassword);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if(passwordTxt.Text != passwordConfirmTxt.Text)
            {
                Toast.MakeText(this, "Passwords do not match", ToastLength.Short).Show();
                return;
            }

            if(BurgerLightAPI.RegisterUser(usernameTxt.Text, passwordTxt.Text, out string eMsg))
            {
                Toast.MakeText(this, "Successfully registered user!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
                Finish();
                return;
            }


            Toast.MakeText(this, eMsg, ToastLength.Short).Show();
        }
    }
}