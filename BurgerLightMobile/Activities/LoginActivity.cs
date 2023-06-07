using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using BurgerLightMobile.API;
using BurgerLightMobile.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.Activities
{
    [Activity(Label = "Login", Theme = "@style/LoginTheme")]
    public class LoginActivity : AppCompatActivity
    {
        private bool loggedIn;
        private ImageButton passwordVisibilityBtn;
        private EditText passwordEditText;
        private Button loginBtn;
        private Button backBtn;
        private EditText usernameText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_layout);

            loggedIn = false;
            //initialize controls
            usernameText = FindViewById<EditText>(Resource.Id.loginUsername);

            loginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            loginBtn.Click += LoginBtn_Click;

            passwordVisibilityBtn = FindViewById<ImageButton>(Resource.Id.passwordVisibilityButton);
            passwordVisibilityBtn.Click += PasswordVisibilityBtn_Click;

            passwordEditText = FindViewById<EditText>(Resource.Id.loginPassword);
            backBtn = FindViewById<Button>(Resource.Id.BackBtnLogin);
            backBtn.Click += BackBtn_Click;
        }

        protected override void OnStart()
        {
            base.OnStart();

            //logout user if user pressed back btn
            if (loggedIn)
            {
                loggedIn = false;
                BurgerLightAPI.LogoutUser(out _);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void PasswordVisibilityBtn_Click(object sender, EventArgs e)
        {
            if (passwordEditText.InputType == InputTypes.TextVariationVisiblePassword)
            {
                //set pass to not visible state
                passwordEditText.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText;
                passwordVisibilityBtn.SetImageResource(Resource.Drawable.password_invisible_eye);
            }
            else
            {
                //set pass to visible state
                passwordEditText.InputType = InputTypes.TextVariationVisiblePassword;
                passwordVisibilityBtn.SetImageResource(Resource.Drawable.password_visible_eye);
            }

            passwordEditText.SetSelection(passwordEditText.Text.Length);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            LoginResponse r = BurgerLightAPI.LoginUser(usernameText.Text, passwordEditText.Text, out string eMsg);
            if (r != null)
            {
                //start main app
                usernameText.Text = string.Empty;
                passwordEditText.Text = string.Empty;
                loggedIn = true;
                Intent t = new Intent(this, typeof(MainActivity));
                t.PutExtra("cartcount", r.carttotal.ToString());
                StartActivity(t);
                return;
            }

            Toast.MakeText(this, eMsg, ToastLength.Short).Show();
        }
    }
}