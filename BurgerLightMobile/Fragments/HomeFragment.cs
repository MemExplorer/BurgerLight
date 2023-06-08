
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurgerLightMobile.Fragments
{
    public class HomeFragment : Fragment
    {
        private ImageButton A;
        private ImageButton B;
        private ImageButton C;
        private ImageButton D;
        private TextView Name;
        private TextView Price;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.content_main, container, false);

            A = view.FindViewById<ImageButton>(Resource.Id.imageButtonA);
            B = view.FindViewById<ImageButton>(Resource.Id.imageButtonB);
            C = view.FindViewById<ImageButton>(Resource.Id.imageButtonC);
            D = view.FindViewById<ImageButton>(Resource.Id.imageButtonD);
            Name = view.FindViewById<TextView>(Resource.Id.textViewX);
            Price = view.FindViewById<TextView>(Resource.Id.textViewY);

            A.Click += ImageA_Click;
            B.Click += ImageB_Click;
            C.Click += ImageC_Click;
            D.Click += ImageD_Click;


            // Use this to return your custom view for this Fragment
            return view;
        }
        private void ImageA_Click(object sender, EventArgs e)
        {
            Name.Text = "Hamburger";
            Price.Text = "Php 69.00";
        }

        private void ImageB_Click(object sender, EventArgs e)
        {
            Name.Text = "Chickenburger";
            Price.Text = "Php 79.00";
        }

        private void ImageC_Click(object sender, EventArgs e)
        {
            Name.Text = "Borgir";
            Price.Text = "Php 99.00";
        }

        private void ImageD_Click(object sender, EventArgs e)
        {
            Name.Text = "BurgerLight Special";
            Price.Text = "Php 199.00";
        }
    }
}