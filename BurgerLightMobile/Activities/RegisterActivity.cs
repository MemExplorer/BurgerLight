﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using BurgerLightMobile.API;
using BurgerLightMobile.API.Models;
using System;

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

            APIResponse<string> r;
            if (BurgerLightAPI.RegisterUser(usernameTxt.Text.ToLower(), passwordTxt.Text, out r))
            {
                Toast.MakeText(this, "Successfully registered user!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
                Finish();
                return;
            }


            Toast.MakeText(this, r.GetMessage(), ToastLength.Short).Show();
        }
    }
}