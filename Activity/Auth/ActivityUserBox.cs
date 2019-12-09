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
using static GeoGeometry.Model.Box.SmartBox;
using Android.Gms.Location;
using GeoGeometry.Model.GPSLocation;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityUserBox")]
    class ActivityUserBox : AppCompatActivity
    {
        private Button btn_exit_;

        private Button btn_change_order;

        private Button btn_pay;

        private Button btn_lock_unlock_door;

        private EditText s_situation_loaded_container;

        private Button btn_pass_delivery_service;

        private EditText s_user;

        private EditText container_name;

        private EditText s_cost;

        private EditText s_pin_access_code;

        private EditText s_lock_unlock_door;

        private EditText s_weight;

        private EditText s_temperature;

        private EditText s_light;

        private EditText s_humidity;

        private static EditText s_longitude;

        private static EditText s_latitude;

        private static EditText s_payment;

        private string a_situation;

        private ProgressBar preloader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_user_box);




            btn_change_order = FindViewById<Button>(Resource.Id.btn_change_order);
            btn_exit_ = FindViewById<Button>(Resource.Id.btn_exit_);
            btn_pay = FindViewById<Button>(Resource.Id.btn_pay);
            btn_lock_unlock_door = FindViewById<Button>(Resource.Id.btn_lock_unlock_door);
            s_situation_loaded_container = FindViewById<EditText>(Resource.Id.s_situation_loaded_container);
            btn_pass_delivery_service = FindViewById<Button>(Resource.Id.btn_pass_delivery_service);
            s_user = FindViewById<EditText>(Resource.Id.s_user);
            container_name = FindViewById<EditText>(Resource.Id.container_name);
            s_cost = FindViewById<EditText>(Resource.Id.s_cost);
            s_payment = FindViewById<EditText>(Resource.Id.s_payment);
            s_lock_unlock_door = FindViewById<EditText>(Resource.Id.s_lock_unlock_door);
            s_pin_access_code = FindViewById<EditText>(Resource.Id.s_pin_access_code);
            s_weight = FindViewById<EditText>(Resource.Id.s_weight);
            s_temperature = FindViewById<EditText>(Resource.Id.s_temperature);
            s_light = FindViewById<EditText>(Resource.Id.s_light);
            s_humidity = FindViewById<EditText>(Resource.Id.s_humidity);
            s_longitude = FindViewById<EditText>(Resource.Id.s_longitude);
            s_latitude = FindViewById<EditText>(Resource.Id.s_latitude);
            preloader = FindViewById<ProgressBar>(Resource.Id.preloader);


            //s_user.Focusable = false;
            //s_user.LongClickable = false;
            //container_name.Focusable = false;
            //container_name.LongClickable = false;
            //s_latitude.Focusable = false;
            //s_latitude.LongClickable = false;
            //s_longitude.Focusable = false;
            //s_longitude.LongClickable = false;
            //s_payment.Focusable = false;
            //s_payment.LongClickable = false;
            //s_cost.Focusable = false;
            //s_cost.LongClickable = false;
            //s_situation_loaded_container.Focusable = false;
            //s_situation_loaded_container.LongClickable = false;
            //s_pin_access_code.Focusable = false;
            //s_pin_access_code.LongClickable = false;
            //s_lock_unlock_door.Focusable = false;
            //s_lock_unlock_door.LongClickable = false;
            //s_weight.Focusable = false;
            //s_weight.LongClickable = false;
            //s_temperature.Focusable = false;
            //s_temperature.LongClickable = false;
            //s_light.Focusable = false;
            //s_light.LongClickable = false;
            //s_humidity.Focusable = false;
            //s_humidity.LongClickable = false;

            string dir_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            if (File.Exists(@"" + dir_path + "box_data.txt"))
            {
                string[] strok = File.ReadAllLines(dir_path + "box_data.txt");

                if (strok.Length != 0)
                {
                    GetInfoAboutBox(dir_path);
                }

            }
            BuildLocationRequest();
            BuildLocationCallBack();

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);

            //ResetUser();
            fusedLocationProviderClient.RequestLocationUpdates(locationRequest,
                locationCallback, Looper.MyLooper());

            //var telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            //var signalStrengthListener = new SignalStrength();
            //_getGsmSignalStrengthButton.Click += DisplaySignalStrength;
            string id_page = Intent.GetStringExtra("idMethod");// !!!
            switch (id_page)
            {
                case "1":
                case "2": //Получение инфы контейнера
                    {
                        GetInfoAboutBox(dir_path);// метод получения данных с контейнера 
                    }
                    break;
                case "3":
                    {

                    }
                    break;
            }


            btn_change_order.Click += async delegate
            {
                try
                {
                    Intent ContainerSelectionActivty = new Intent(this, typeof(Auth.ContainerSelection));
                    StartActivity(ContainerSelectionActivty);

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };



            btn_exit_.Click += async delegate
            {
                ClearField();
                Intent ActivityMain = new Intent(this, typeof(MainActivity));
                StartActivity(ActivityMain);
            };

            

        }
        

        private async void GetInfoAboutBox(string dir_path)
        {
            try
            {
                ContainerResponse container = new ContainerResponse();

                //извлечение данных контейнера из файла
                //using (FileStream fs = new FileStream(dir_path + "box_data.txt", FileMode.OpenOrCreate))
                //{
                //    container = await System.Text.Json.JsonSerializer.DeserializeAsync<ContainerResponse>(fs);
                //}

                //пример чтения данных с файла
                string file_data_remember;
                using (FileStream file = new FileStream(dir_path + "box_data.txt", FileMode.Open, FileAccess.Read))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[file.Length];
                    // считываем данные
                    file.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    file_data_remember = Encoding.Default.GetString(array);
                    file.Close();
                }

                container = JsonConvert.DeserializeObject<ContainerResponse>(file_data_remember);

                var myHttpClient = new HttpClient();

                var uri = new Uri("http://iot.tmc-centert.ru/api/container/getbox?id=" + container.SmartBoxId);
                HttpResponseMessage response = await myHttpClient.GetAsync(uri);

                AuthApiData<BoxDataResponse> o_data = new AuthApiData<BoxDataResponse>();

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                o_data = JsonConvert.DeserializeObject<AuthApiData<BoxDataResponse>>(s_result);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (o_data.Status == "0")
                    {
                        Toast.MakeText(this, o_data.Message, ToastLength.Long).Show();
                        BoxDataResponse exported_data = new BoxDataResponse();
                        exported_data = o_data.ResponseData;

                        StaticBox.AddInfoBox(exported_data);
                        //добавляем инфу о найденном контейнере
                        container_name.Text = container.Name;
                        s_temperature.Text = exported_data.Temperature.ToString();
                        s_light.Text = exported_data.Light.ToString();
                        s_humidity.Text = exported_data.Wetness.ToString();
                        s_weight.Text = exported_data.Weight.ToString();
                        s_pin_access_code.Text = (exported_data.Code == null) ? "0000" : exported_data.Code;// !!!!

                        //var boxState = s_open_close_container.Text;
                        //var doorState = s_lock_unlock_door.Text;

                        if (exported_data.BoxState == ContainerState.onBase)
                        {
                            a_situation = "На складе";
                        }
                        else if (exported_data.BoxState == ContainerState.onCar)
                        {
                            a_situation = "На автомобиле";
                        }
                        else if (exported_data.BoxState == ContainerState.onConsignee)
                        {
                            a_situation = "Выгруженным у грузоотправителя";
                        }
                        else if (exported_data.BoxState == ContainerState.onShipper)
                        {
                            a_situation = "После разгрузки у грузополучателя";
                        }                     
                    }
                    else
                    {
                        Toast.MakeText(this, o_data.Message, ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
            }

        }


        FusedLocationProviderClient fusedLocationProviderClient;
        LocationRequest locationRequest;
        LocationCallback locationCallback;
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


        internal class DriverLocationCallBack : LocationCallback // !!!!
        {
            private ActivityUserBox activityUserBoxy;

            public DriverLocationCallBack(ActivityUserBox activityUserBoxy)
            {
                this.activityUserBoxy = activityUserBoxy;
            }

            public override void OnLocationResult(LocationResult result)
            {
                base.OnLocationResult(result);

                StaticBox.Latitude = result.LastLocation.Latitude;
                StaticBox.Longitude = result.LastLocation.Longitude;
                StaticBox.Signal = 0;
                StaticBox.Date = DateTime.Now;

                s_longitude.Text = result.LastLocation.Latitude.ToString();
                s_latitude.Text = result.LastLocation.Longitude.ToString();
            }
        }

        void ClearField()
        {
            s_user.Text = "";
            container_name.Text = "";
            s_cost.Text = "";
            s_payment.Text = "";
            s_lock_unlock_door.Text = "";
            s_pin_access_code.Text = "";
            s_weight.Text = "";
            s_temperature.Text = "";
            s_light.Text = "";
            s_humidity.Text = "";
            s_longitude.Text = "";
            s_latitude.Text = "";
        }

    }
}