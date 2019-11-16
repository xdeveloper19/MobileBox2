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

        private EditText s_signal_strength;

        private EditText s_longitude;

        private EditText s_latitude;

        private EditText s_date;

        private EditText s_time;

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
            s_date = FindViewById<EditText>(Resource.Id.s_date);
            s_time = FindViewById<EditText>(Resource.Id.s_time);
            preloader = FindViewById<ProgressBar>(Resource.Id.preloader);

            string dir_path = "/storage/emulated/0/Android/data/GeoGeometry.GeoGeometry/files/";
            //var telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            //var signalStrengthListener = new SignalStrength();

          

            //_getGsmSignalStrengthButton.Click += DisplaySignalStrength;

            btn_change_container.Click += async delegate
            {
                try
                {
                    Intent ContainerSelectionActivty = new Intent(this, typeof(Auth.ContainerSelection));
                    StartActivity(ContainerSelectionActivty);
                    string name = "";
                    FileStream fs = new FileStream(dir_path + "box_data.txt", FileMode.OpenOrCreate);
                    
                    ContainerResponse container = await System.Text.Json.JsonSerializer.DeserializeAsync<ContainerResponse>(fs);
                        
                    


                    var myHttpClient = new HttpClient();
                    //айди контейнера
                    var uri = "http://iot.tmc-centert.ru/api/container/getbox?id=" + container.SmartBoxId;

                    HttpResponseMessage response = await myHttpClient.GetAsync(uri);

                    AuthApiData<BoxDataResponse> o_data = new AuthApiData<BoxDataResponse>();

                    string s_result;
                    using (HttpContent responseContent = response.Content)
                    {
                        s_result = await responseContent.ReadAsStringAsync();
                    }

                    //здесь допишешь сам га
                    o_data = JsonConvert.DeserializeObject<AuthApiData<BoxDataResponse>>(s_result);
                    var o_boxes_data = o_data.ResponseData;

                    StaticBox.AddInfoBox(o_boxes_data);
                    //добавляем инфу о найденном контейнере
                    container_id.Text = o_boxes_data.Id;                  
                    s_open_close_container.Text = o_boxes_data.IsOpenedBox.ToString();                 
                    s_weight.Text = o_boxes_data.Weight.ToString();
                    s_temperature.Text = o_boxes_data.Temperature.ToString();
                    s_light.Text = o_boxes_data.Light.ToString();
                    s_humidity.Text = o_boxes_data.Wetness.ToString();
                    s_battery.Text = o_boxes_data.BatteryPower.ToString();
                    if (o_boxes_data.IsOpenedDoor == false)
                    {
                        btn_lock_unlock_door.Text = "заблокирована";
                    }
                    else
                    {
                        btn_lock_unlock_door.Text = "разблокирована";
                    }                   

                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
                

            };
            btn_open_close_container.Click += async delegate
            {
                try
                {

                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };
            btn_lock_unlock_door.Click += async delegate
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
            };
            btn_change_parameters.Click += async delegate
            {
                try
                {
                    preloader.Visibility = Android.Views.ViewStates.Visible;



                    StaticBox.SmartBoxId = container_id.Text;
                    StaticBox.Temperature = Convert.ToDouble(s_temperature.Text);
                    StaticBox.Weight = Convert.ToDouble(s_weight.Text);
                    StaticBox.Light = Convert.ToInt32(s_light.Text);
                    StaticBox.Wetness = Convert.ToDouble(s_humidity.Text);
                    StaticBox.Code = s_pin_access_code.Text;
                    StaticBox.IsOpenedBox = (s_open_close_container.Text == "открыт") ? true : false;
                    //Situation = s_situation.Text,
                    StaticBox.IsOpenedDoor = (s_lock_unlock_door.Text == " открыта дверь") ? true : false;
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

                    var uri = ("http://iot-tmc-cen.1gb.ru/api/container/editbox?id=" + StaticBox.SmartBoxId +"&IsOpenedBox="+ StaticBox.IsOpenedBox + "&IsOpenedDoor=" + StaticBox.IsOpenedDoor + "&Weight=" + StaticBox.Weight + "&Light=" + StaticBox.Light + "&Code=" + StaticBox.Code + "&Temperature=" + StaticBox.Temperature + "&Wetness=" + StaticBox.Wetness + "&BatteryPower=" + StaticBox.BatteryPower);


                    HttpResponseMessage response = await myHttpClient.PutAsync(uri.ToString(), new StringContent(JsonConvert.SerializeObject(container), Encoding.UTF8, "application/json"));

                    AuthApiData<BaseResponseObject> o_data = new AuthApiData<BaseResponseObject>();

                    string s_result;
                    using (HttpContent responseContent = response.Content)
                    {
                        s_result = await responseContent.ReadAsStringAsync();
                    }

                    o_data = JsonConvert.DeserializeObject<AuthApiData<BaseResponseObject>>(s_result);
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
            s_date.Text = "";
            s_time.Text = "";
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


