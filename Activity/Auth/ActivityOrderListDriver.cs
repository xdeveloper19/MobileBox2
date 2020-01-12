using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityOrderListDriver")]

   
    class ActivityOrderListDriver : AppCompatActivity
    {
        private Button btn_order1;

        private Button btn_order2;

        private Button btn_order3;

        private Button btn_order4;

        private Button btn_order15;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.activity_order_list_driver);
        }
    }
}