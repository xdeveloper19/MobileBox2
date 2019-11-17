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

namespace GeoGeometry.Activity.Auth
{

    [Activity(Label = "DriverActivity")]

    public class DriverActivity : AppCompatActivity
    {

        private Button btn_exit_;

        private Button btn_change_container;

        private Button btn_open_close_container;

        private Button btn_lock_unlock_door;

        private Button btn_change_parameters;

        private Button btn_transfer_access;

        private Button btn_change_pin_code;

        private EditText s_user;

        private EditText container_id;

        private EditText s_situation;

        private EditText s_open_close_container;

        private EditText s_lock_unlock_door;

        private EditText s_pin_access_code;

        private EditText s_weight;

        private EditText s_temperature;

        private EditText s_light;

        private EditText s_humidity; 

        private EditText s_battery;

        private static EditText s_signal_strength;

        private static EditText s_longitude;

        private static EditText s_latitude;

        private static EditText s_date_time;

        private ProgressBar preloader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_driver);

            
            btn_change_container = FindViewById<Button>(Resource.Id.btn_change_container);
            btn_exit_ = FindViewById<Button>(Resource.Id.btn_exit_);
            btn_open_close_container = FindViewById<Button>(Resource.Id.btn_open_close_container);
            btn_lock_unlock_door = FindViewById<Button>(Resource.Id.btn_lock_unlock_door);
            btn_change_parameters = FindViewById<Button>(Resource.Id.btn_change_parameters);
            btn_transfer_access = FindViewById<Button>(Resource.Id.btn_transfer_access);
            btn_change_pin_code = FindViewById<Button>(Resource.Id.btn_change_pin_code);
            s_user = FindViewById<EditText>(Resource.Id.s_user);
            container_id = FindViewById<EditText>(Resource.Id.container_id);
            s_situation = FindViewById<EditText>(Resource.Id.s_situation);
            s_open_close_container = FindViewById<EditText>(Resource.Id.s_open_close_container);
            s_lock_unlock_door = FindViewById<EditText>(Resource.Id.s_lock_unlock_door);
            s_pin_access_code = FindViewById<EditText>(Resource.Id.s_pin_access_code);
            s_weight = FindViewById<EditText>(Resource.Id.s_weight);
            s_temperature = FindViewById<EditText>(Resource.Id.TemperatureEdit);
            s_light = FindViewById<EditText>(Resource.Id.s_light);
            s_humidity = FindViewById<EditText>(Resource.Id.s_humidity);
            s_battery = FindViewById<EditText>(Resource.Id.s_battery);
            s_signal_strength = FindViewById<EditText>(Resource.Id.s_signal_strength);
            s_longitude = FindViewById<EditText>(Resource.Id.s_longitude);
            s_latitude = FindViewById<EditText>(Resource.Id.s_latitude);
            s_date_time = FindViewById<EditText>(Resource.Id.s_date_time);
            preloader = FindViewById<ProgressBar>(Resource.Id.preloader);

            string dir_path = "/storage/emulated/0/Android/data/GeoGeometry.GeoGeometry/files/";
            
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

            //var telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            //var signalStrengthListener = new SignalStrength();
            //_getGsmSignalStrengthButton.Click += DisplaySignalStrength;
            string id_page = Intent.GetStringExtra("idAction");
            switch (id_page)
            {
                case "1": 
                case "2": //Получение инфы контейнера
                    {
                        GetInfoAboutBox(dir_path);
                    }
                    break;
                case "3": 
                    {
                        
                    }
                    break;
            }

            //переход на форму выбора контейнера
            btn_change_container.Click += async delegate
            {
                
                try
                {
                    Intent ContainerSelectionActivty = new Intent(this, typeof(Auth.ContainerSelection));
                    StartActivity(ContainerSelectionActivty);

                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };

            //изменение состояния контейнера
            btn_open_close_container.Click += async delegate
            {
                try
                {
                    if (s_open_close_container.Text == "закрыт")
                        s_open_close_container.Text = "раскрыт";
                    else
                        s_open_close_container.Text = "закрыт";
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };

            //изменение состояния дверей
            btn_lock_unlock_door.Click += async delegate
            {
                try
                {
                    if (s_lock_unlock_door.Text == "заблокирована")
                        s_lock_unlock_door.Text = "разблокирована";
                    else
                        s_lock_unlock_door.Text = "заблокирована";
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };

            //изменение ПИН-кода, очистка полей
            btn_change_pin_code.Click += async delegate
            {
                try
                {
                    s_pin_access_code.Text = "";
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };

            //редактирование данных контейнера
            btn_change_parameters.Click += async delegate
            {
                try
                {
                    preloader.Visibility = Android.Views.ViewStates.Visible;

                    StaticBox.Temperature = Convert.ToDouble(s_temperature.Text);
                    StaticBox.Weight = Convert.ToDouble(s_weight.Text);
                    StaticBox.Light = Convert.ToInt32(s_light.Text);
                    StaticBox.Wetness = Convert.ToDouble(s_humidity.Text);
                    StaticBox.Code = s_pin_access_code.Text;
                    StaticBox.IsOpenedBox = (s_open_close_container.Text == "раскрыт") ? true : false;
                    //Situation = s_situation.Text,
                    StaticBox.IsOpenedDoor = (s_lock_unlock_door.Text == "разблокирована") ? true : false;
                    StaticBox.BatteryPower = Convert.ToDouble(s_battery.Text);

                    SmartBox container = new SmartBox
                    {
                        Id = StaticBox.SmartBoxId,
                        Temperature = StaticBox.Temperature,
                        Weight = StaticBox.Weight,
                        Light = StaticBox.Light,
                        Wetness = StaticBox.Wetness,
                        Code = StaticBox.Code,
                        IsOpenedBox = StaticBox.IsOpenedBox,
                        //Situation = s_situation.Text,
                        IsOpenedDoor = StaticBox.IsOpenedDoor,
                        BatteryPower = StaticBox.BatteryPower
                    };

                    var myHttpClient = new HttpClient();

                    var uri = ("http://iot-tmc-cen.1gb.ru/api/container/editbox?id=" + container.Id +"&IsOpenedBox="+ container.IsOpenedBox + "&IsOpenedDoor=" + container.IsOpenedDoor + "&Weight=" + container.Weight + "&Light=" + container.Light + "&Code=" + container.Code + "&Temperature=" + container.Temperature + "&Wetness=" + container.Wetness + "&BatteryPower=" + container.BatteryPower);


                    HttpResponseMessage response = await myHttpClient.PutAsync(uri.ToString(), new StringContent(JsonConvert.SerializeObject(container), Encoding.UTF8, "application/json"));

                    AuthApiData<BaseResponseObject> o_data = new AuthApiData<BaseResponseObject>();

                    string s_result;
                    using (HttpContent responseContent = response.Content)
                    {
                        s_result = await responseContent.ReadAsStringAsync();
                    }

                    o_data = JsonConvert.DeserializeObject<AuthApiData<BaseResponseObject>>(s_result);
                    
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, o_data.Message, ToastLength.Long).Show();
                    }
                    Intent.PutExtra("idAction", "2");
                    //перезапуск страницы
                    Recreate();
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

        /// <summary>
        /// сбор информации о контейнере
        /// </summary>
        /// <param name="dir_path"></param>
        private async void GetInfoAboutBox(string dir_path)
        {
            try
            {
                ContainerResponse container = new ContainerResponse();

                //извлечение данных контейнера из файла
                using (FileStream fs = new FileStream(dir_path + "box_data.txt", FileMode.OpenOrCreate))
                {
                    container = await System.Text.Json.JsonSerializer.DeserializeAsync<ContainerResponse>(fs);
                }

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
                        container_id.Text = exported_data.Id;
                        s_open_close_container.Text = (exported_data.IsOpenedBox == false) ? "закрыт" : "раскрыт";
                        s_weight.Text = exported_data.Weight.ToString();
                        s_lock_unlock_door.Text = (exported_data.IsOpenedDoor == false) ? "заблокирована" : "разблокирована";

                        var boxState = s_open_close_container.Text;
                        var doorState = s_lock_unlock_door.Text;

                        s_situation.Text = "Контейнер " + boxState + ". Дверь " + doorState + ". Ожидает передачи доступа заказчику.";
                        s_temperature.Text = exported_data.Temperature.ToString();
                        s_light.Text = exported_data.Light.ToString();
                        s_pin_access_code.Text = (exported_data.Code == null) ? "0000" : exported_data.Code;
                        s_humidity.Text = exported_data.Wetness.ToString();
                        s_battery.Text = exported_data.BatteryPower.ToString();
                        btn_lock_unlock_door.Text = "Заблокировать/Разблокировать";
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

        /// <summary>
        /// Подключение к карте api ключ. 
        /// https://console.developers.google.com/apis/credentials?project=geogeometry&hl=RU&supportedpurview=project
        /// информация https://docs.microsoft.com/ru-ru/xamarin/android/platform/maps-and-location/maps/maps-api#google-maps-api-prerequisites
        /// </summary>

        internal class DriverLocationCallBack : LocationCallback
        {
            private DriverActivity driverActivity;

            public DriverLocationCallBack(DriverActivity driverActivity)
            {
                this.driverActivity = driverActivity;
            }

            public override async void OnLocationResult(LocationResult result)
            {
                base.OnLocationResult(result);

                StaticBox.Latitude = result.LastLocation.Latitude;
                StaticBox.Longitude = result.LastLocation.Longitude;
                StaticBox.Signal = 8;
                StaticBox.Date = DateTime.Now;

                s_longitude.Text = result.LastLocation.Latitude.ToString();
                s_latitude.Text = result.LastLocation.Longitude.ToString();
                s_date_time.Text = DateTime.Now.ToString();
                s_signal_strength.Text = "Хороший";

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
                var uri = new Uri("http://iot-tmc-cen.1gb.ru/api/container/setcontainerlocation/");

                //json структура.
                var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "Id", gpsLocation.id },
                { "Lon1", gpsLocation.lon1.ToString()},
                { "Lat1", gpsLocation.lat1.ToString()}
            });

                HttpResponseMessage response = await myHttpClient.PostAsync(uri.ToString(), formContent);
                AuthApiData<BaseResponseObject> o_data = new AuthApiData<BaseResponseObject>();

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                o_data = JsonConvert.DeserializeObject<AuthApiData<BaseResponseObject>>(s_result);

                
        }
        }
        
        //очистка всех полей
        void ClearField()
        {
            s_user.Text = "";
            container_id.Text = "";
            s_situation.Text = "";
            s_open_close_container.Text = "";
            s_lock_unlock_door.Text = "";
            s_pin_access_code.Text = "";
            s_weight.Text = "";
            s_temperature.Text = "";
            s_light.Text = "";
            s_humidity.Text = "";
            s_battery.Text = "";
            s_signal_strength.Text = "";
            s_longitude.Text = "";
            s_latitude.Text = "";
            s_date_time.Text = "";
        }

        //void DisplaySignalStrength(object sender, EventArgs e)
        //{
        //    var telephonyManager.Listen(signalStrengthListener, PhoneStateListenerFlags.SignalStrengths);
        //    var signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;
        //}

        //void HandleSignalStrengthChanged(int strength)
        //{
        //    // We want this to be a one-shot thing when the button is pushed. Make sure to unhook everything
        //    var signalStrengthListener.SignalStrengthChanged -= HandleSignalStrengthChanged;
        //    var telephonyManager.Listen(signalStrengthListener, PhoneStateListenerFlags.None);

        //    // Update the UI with text and an image.
        //    var gmsStrengthImageView.SetImageLevel(strength);
        //    gmsStrengthTextView.Text = string.Format("GPS Signal Strength ({0}):", strength);
        //}
    }
}


