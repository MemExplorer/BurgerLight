
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace BurgerLightMobile
{
    [Activity(Label = "Check out", Theme = "@style/LoginTheme")]
    public class CheckoutActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.checkoutLayout);
            // Create your application here
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}