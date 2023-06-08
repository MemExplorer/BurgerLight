using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using BurgerLightMobile.Fragments;
using Google.Android.Material.Navigation;
using Fragment = AndroidX.Fragment.App.Fragment;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace BurgerLightMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        internal static MainActivity Instance;
        private Button LogoutBtn;
        private ImageButton CartBtn;
        public TextView CartCount;
        public TextView usernameTxtView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            SetContentFrame(Resource.Id.nav_home);

            LogoutBtn = FindViewById<Button>(Resource.Id.logoutBtn);
            LogoutBtn.Click += LogoutBtn_Click;

            CartBtn = FindViewById<ImageButton>(Resource.Id.CartBtn);
            CartBtn.Click += CartBtn_Click;
            CartCount = FindViewById<TextView>(Resource.Id.CartItemCount);
            CartCount.Text = Intent.GetStringExtra("cartcount");

            View headerView = navigationView.GetHeaderView(0);
            usernameTxtView = headerView.FindViewById<TextView>(Resource.Id.usernameMain);
            usernameTxtView.Text = Intent.GetStringExtra("username");

            SetTab(Resource.Id.nav_home);
        }

        protected override void OnStart()
        {
            base.OnStart();
            Instance = this;
        }

        private void CartBtn_Click(object sender, EventArgs e)
        {
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.Menu.GetItem(2).SetChecked(true);
            SetContentFrame(Resource.Id.nav_orders);
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        internal void SetTab(int id)
        {
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            switch (id)
            {
                case Resource.Id.nav_home:
                    navigationView.Menu.GetItem(0).SetChecked(true);
                    break;
                case Resource.Id.nav_menu:
                    navigationView.Menu.GetItem(1).SetChecked(true);
                    break;
                case Resource.Id.nav_orders:
                    navigationView.Menu.GetItem(2).SetChecked(true);
                    break;
                default:
                    throw new NotSupportedException();
            }

            SetContentFrame(id);
        }

        private void SetContentFrame(int id)
        {
            Fragment f = null;

            switch(id) {
                case Resource.Id.nav_home:
                    f = new HomeFragment();
                    break;
                case Resource.Id.nav_menu:
                    f = new MenuFragment();
                    break;
                case Resource.Id.nav_orders:
                    f = new OrdersFragment();
                    break;
                default:
                    throw new NotSupportedException();
            }

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, f).CommitAllowingStateLoss();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            SetContentFrame(id);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

