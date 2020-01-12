using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoGeometry.Model.Auth;
using Android.App;
using Android.Content;
using Android.OS;
using System.Text.Json;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using GeoGeometry.Container;
using System.IO;
using GeoGeometry.Model.User;
using GeoGeometry.Model.Box;
using GeoGeometry.Model;
using Android.Telephony;
using Android.Gms.Location;
using GeoGeometry.Model.GPSLocation;
using Android.Gms.Maps;
using static GeoGeometry.Model.Box.SmartBox;
using Android.Gms.Maps.Model;
using System.Globalization;
using GeoGeometry.Activity.Menu;
using static GeoGeometry.Activity.Auth.DriverActivity;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityOrderList")]
    class ActivityOrderList : AppCompatActivity, IOnMapReadyCallback
    {
        private EditText s_order;

        private EditText s_task;

        private Button btn_abort_order;

        private Button btn_performed;

        GoogleMap _googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_task_list);

            s_order = FindViewById<EditText>(Resource.Id.s_order);
            s_task = FindViewById<EditText>(Resource.Id.s_task);
            btn_abort_order = FindViewById<Button>(Resource.Id.btn_abort_order);
            btn_performed = FindViewById<Button>(Resource.Id.btn_performed);

            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.fragmentMap);
            mapFragment.GetMapAsync(this);

            s_order.Focusable = false;
            s_order.LongClickable = false;
            s_task.Focusable = false;
            s_task.LongClickable = false;

            BuildLocationRequest();
            BuildLocationCallBack();

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);

            //ResetUser();
            fusedLocationProviderClient.RequestLocationUpdates(locationRequest,
                locationCallback, Looper.MyLooper());
        }

        FusedLocationProviderClient fusedLocationProviderClient;
        LocationRequest locationRequest;
        LocationCallback locationCallback;
        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;////11111

            MarkerOptions markerOptions = new MarkerOptions();
            LatLng location = new LatLng(StaticBox.Latitude, StaticBox.Longitude);
            markerOptions.SetPosition(location);
            markerOptions.SetTitle("Я здесь");
            googleMap.AddMarker(markerOptions);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(0);
            builder.Tilt(65);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(cameraUpdate);


        }
        private void BuildLocationCallBack()
        {
            locationCallback = new DriverLocationCallBack(this);
        }

        private void BuildLocationRequest()
        {
            locationRequest = new LocationRequest();
            locationRequest.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);
            locationRequest.SetInterval(1000);
            locationRequest.SetFastestInterval(3000);
            locationRequest.SetSmallestDisplacement(10f);
        }

        internal class DriverLocationCallBack : LocationCallback
        {
            private ActivityOrderList driverActivity;

            public DriverLocationCallBack(ActivityOrderList driverActivity)
            {
                this.driverActivity = driverActivity;
            }

            public override async void OnLocationResult(LocationResult result)
            {
                base.OnLocationResult(result);

                try
                {
                    StaticBox.Latitude = result.LastLocation.Latitude;
                    StaticBox.Longitude = result.LastLocation.Longitude;
                    StaticBox.Signal = 0;
                    StaticBox.Date = DateTime.Now;

                   

                    // Получаю информацию о клиенте.
                    BoxLocation gpsLocation = new BoxLocation
                    {
                        id = StaticBox.SmartBoxId,
                        lat1 = StaticBox.Latitude,
                        lon1 = StaticBox.Longitude,
                        signal = StaticBox.Signal,
                        date = StaticBox.Date
                    };



                    var myHttpClient = new HttpClient();
                    var uri = new Uri("http://iot-tmc-cen.1gb.ru/api/container/setcontainerlocation?id=" + gpsLocation.id + "&lat1=" + gpsLocation.lat1 + "&lon1=" + gpsLocation.lon1 + "&signal=" + gpsLocation.signal + "&date=" + gpsLocation.date);
                    var uri2 = new Uri("http://81.177.136.11:8003/geo?id=" + gpsLocation.id + "&lat1=" + gpsLocation.lat1 + "&lon1=" + gpsLocation.lon1 + "&signal=" + gpsLocation.signal + "&date=" + gpsLocation.date);
                    //json структура.
                    var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "Id", gpsLocation.id },
                { "Lon1", gpsLocation.lon1.ToString()},
                { "Lat1", gpsLocation.lat1.ToString()},
                { "Signal", "0"},
                { "Date", DateTime.Now.ToString()}
            });

                    HttpResponseMessage response = await myHttpClient.PostAsync(uri.ToString(), formContent);// !!!!
                    HttpResponseMessage responseFromAnotherServer = await myHttpClient.PostAsync(uri2.ToString(), new StringContent(JsonConvert.SerializeObject(gpsLocation), Encoding.UTF8, "application/json"));
                    AuthApiData<BaseResponseObject> o_data = new AuthApiData<BaseResponseObject>();

                    string s_result;
                    using (HttpContent responseContent = response.Content)
                    {
                        s_result = await responseContent.ReadAsStringAsync();
                    }

                    string s_result_from_another_server;
                    using (HttpContent responseContent = responseFromAnotherServer.Content)
                    {
                        s_result_from_another_server = await responseContent.ReadAsStringAsync();
                    }

                    o_data = JsonConvert.DeserializeObject<AuthApiData<BaseResponseObject>>(s_result);
                }
                catch (Exception ex)
                {

                    throw;
                }


            }
        }

    }
}