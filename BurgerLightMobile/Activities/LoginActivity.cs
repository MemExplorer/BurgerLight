using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
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
        private ImageButton passwordVisibilityBtn;
        private EditText passwordEditText;
        private Button loginBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_layout);

            //initialize controls
            loginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            loginBtn.Click += LoginBtn_Click;

            passwordVisibilityBtn = FindViewById<ImageButton>(Resource.Id.passwordVisibilityButton);
            passwordVisibilityBtn.Click += PasswordVisibilityBtn_Click;

            passwordEditText = FindViewById<EditText>(Resource.Id.loginPassword);
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
            //start main app
            Intent t = new Intent(this, typeof(MainActivity));
            StartActivity(t);
        }
    }
}