//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using GeoGeometry.Model.Auth;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using System.Text.Json;
//using Android.Runtime;
//using Android.Support.V7.App;
//using Android.Views;
//using Android.Widget;
//using System.Net.Http;
//using Newtonsoft.Json;
//using System.Net;
//using GeoGeometry.Container;
//using System.IO;
//using GeoGeometry.Model.User;
//using GeoGeometry.Model.Box;
//using GeoGeometry.Model;


//namespace GeoGeometry.Activity.Auth
//{
//    [Activity(Label = "ActivityUserBox")]
//    class ActivityUserBox : AppCompatActivity
//    {
//        private Button btn_exit_;

//        private Button btn_change_order;

//        private Button btn_pay;

//        private Button btn_lock_unlock_door;

//        private Spinner btn_situation_loaded_container;

//        private Button btn_pass_delivery_service;

//        private EditText s_user;

//        private EditText s_order_number;

//        private EditText s_longitude;

//        private EditText s_latitude;

//        private EditText s_cost;

//        private EditText s_position_pay;

//        private EditText s_pin_access_code;

//        private EditText s_lock_unlock_door;

//        private EditText s_weight;

//        private EditText s_temperature;

//        private EditText s_light;

//        private EditText s_humidity;
//        private string a_situation;

//        private ProgressBar preloader;

//        private string s_container_status_selection;
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            SetContentView(Resource.Layout.activity_user_box);


//            btn_change_order = FindViewById<Button>(Resource.Id.btn_change_order);
//            btn_exit_ = FindViewById<Button>(Resource.Id.btn_exit_);
//            btn_pay = FindViewById<Button>(Resource.Id.btn_pay);
//            btn_lock_unlock_door = FindViewById<Button>(Resource.Id.btn_lock_unlock_door);
//            btn_situation_loaded_container = FindViewById<Spinner>(Resource.Id.btn_situation_loaded_container);
//            btn_pass_delivery_service = FindViewById<Button>(Resource.Id.btn_pass_delivery_service);
//            s_user = FindViewById<EditText>(Resource.Id.s_user);
//            s_order_number = FindViewById<EditText>(Resource.Id.s_order_number);
//            s_cost = FindViewById<EditText>(Resource.Id.s_cost);
//            s_position_pay = FindViewById<EditText>(Resource.Id.s_position_pay);
//            s_lock_unlock_door = FindViewById<EditText>(Resource.Id.s_lock_unlock_door);
//            s_pin_access_code = FindViewById<EditText>(Resource.Id.s_pin_access_code);
//            s_weight = FindViewById<EditText>(Resource.Id.s_weight);
//            s_temperature = FindViewById<EditText>(Resource.Id.TemperatureEdit);
//            s_light = FindViewById<EditText>(Resource.Id.s_light);
//            s_humidity = FindViewById<EditText>(Resource.Id.VlazhnostEdit);
//            s_longitude = FindViewById<EditText>(Resource.Id.s_longitude);
//            s_latitude = FindViewById<EditText>(Resource.Id.s_latitude);
//            preloader = FindViewById<ProgressBar>(Resource.Id.preloader);

//            btn_situation_loaded_container.Prompt = "Выберите состояние контейнера";
//            btn_situation_loaded_container.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
//            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.a_situation_loaded_container, Android.Resource.Layout.SimpleSpinnerItem);

//            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
//            btn_situation_loaded_container.Adapter = adapter;

//            btn_exit_.Click += async delegate
//            {
//                ClearField();
//                Intent ActivityMain = new Intent(this, typeof(MainActivity));
//                StartActivity(ActivityMain);
//            };


//        }
//        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
//        {
//            var spinner = sender as Spinner;
//            s_container_status_selection = spinner.GetItemAtPosition(e.Position).ToString();
//        }


//        void ClearField()
//        {
//            s_user.Text = "";
//            s_order_number.Text = "";
//            s_cost.Text = "";
//            s_position_pay.Text = "";
//            s_lock_unlock_door.Text = "";
//            s_pin_access_code.Text = "";
//            s_weight.Text = "";
//            s_temperature.Text = "";
//            s_light.Text = "";
//            s_humidity.Text = "";
//            s_longitude.Text = "";
//            s_latitude.Text = "";
//        }

//    }
//}