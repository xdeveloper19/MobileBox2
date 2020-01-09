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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using GeoGeometry.Activity.Menu;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityUserBox")]
    class ActivityUserBox : AppCompatActivity, IOnMapReadyCallback
    {
        private Button btn_exit_;

        private Button btn_change_order;

        private Button btn_pay;

        private Button btn_lock_unlock_door;

        private Button btn_camera;

        private Button btn_pass_delivery_order;

        private EditText s_situation_loaded_container;

        private Button btn_pass_delivery_service;

        private EditText s_user;       

        private EditText container_name;

        private EditText s_cost;

        private TextView status;

        private EditText s_pin_access_code;

        private EditText s_lock_unlock_door;

        private EditText s_weight;

        private EditText s_temperature;

        private ProgressBar progressBar;

        private TextView status_view;

        private EditText s_light;

        private EditText s_humidity;

        private TextView Text3;

        private static EditText s_longitude;

        private static EditText s_latitude;

        private static EditText s_payment;      

        private ProgressBar preloader;

        private RelativeLayout auth_container;

        GoogleMap _googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_user_box);

            StaticMenu.id_page = 4;

            auth_container = FindViewById<RelativeLayout>(Resource.Id.container);
            //var builder = new Android.App.AlertDialog.Builder(this);
            //builder.SetTitle("Operation confirmation");
            //builder.SetMessage("Continue with " + "" + " command?");
            //builder.SetPositiveButton("Yes", (sender, args) => { /* do stuff on OK */ });
            //builder.SetNegativeButton("No", (sender, args) => { cmd = "cancel"; });
            //builder.SetCancelable(false);
            //builder.Show();

            
            Text3 = FindViewById<TextView>(Resource.Id.Text3);
            btn_camera = FindViewById<Button>(Resource.Id.btn_camera);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            status_view = FindViewById<TextView>(Resource.Id.status_view);
            btn_exit_ = FindViewById<Button>(Resource.Id.btn_exit_);
            btn_pay = FindViewById<Button>(Resource.Id.btn_pay);
            btn_lock_unlock_door = FindViewById<Button>(Resource.Id.btn_lock_unlock_door);
            status = FindViewById<TextView>(Resource.Id.status1);
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

            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.fragmentMap);
            mapFragment.GetMapAsync(this);

            container_name.Focusable = false;
            container_name.LongClickable = false;
            s_user.Focusable = false;
            s_user.LongClickable = false;
            s_latitude.Focusable = false;
            s_latitude.LongClickable = false;
            s_longitude.Focusable = false;
            s_longitude.LongClickable = false;
            s_payment.Focusable = false;
            s_payment.LongClickable = false;
            s_cost.Focusable = false;
            s_cost.LongClickable = false;
            
            s_pin_access_code.Focusable = false;
            s_pin_access_code.LongClickable = false;
            s_lock_unlock_door.Focusable = false;
            s_lock_unlock_door.LongClickable = false;
            s_weight.Focusable = false;
            s_weight.LongClickable = false;
            s_temperature.Focusable = false;
            s_temperature.LongClickable = false;
            s_light.Focusable = false;
            s_light.LongClickable = false;
            s_humidity.Focusable = false;
            s_humidity.LongClickable = false;

            string dir_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            if (File.Exists(@"" + dir_path + "box_data.txt"))
            {
                string[] strok = File.ReadAllLines(dir_path + "box_data.txt");

                if (strok.Length != 0)
                {
                    s_payment.Text = "Не оплачено";
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
            //string id_page = Intent.GetStringExtra("idMethod");// !!!
            //switch (id_page)
            //{
            //    case "1":
            //    case "2": //Получение инфы контейнера
            //        {
            //            GetInfoAboutBox(dir_path);// метод получения данных с контейнера 
            //        }
            //        break;
            //    case "3":
            //        {

            //        }
            //        break;
            //}
            
            



            //btn_change_order.Click += async delegate
            //{
            //    try
            //    {
            //        Intent AcrivityApplication = new Intent(this, typeof(Auth.ActivityApplication));
            //        StartActivity(AcrivityApplication);
            //    }
            //    catch (Exception ex)
            //    {
            //        Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
            //    }
            //};

            //изменение состояния дверей
            btn_lock_unlock_door.Click += async delegate
            {
                
                    try
                    {
                        Android.Support.V7.App.AlertDialog alertDialog;
                        List<string> Item = new List<string>();
                        Item.Add("Выгрузка завершена. Контейнер готов к отправке.");


                        var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                        builder.SetTitle("Вы действительно хотите открыть замок контейнера?");

                        bool[] toDownload = { false};
                        builder.SetMultiChoiceItems(Item.ToArray(), toDownload, (sender, e) =>
                        {
                            int index = e.Which;

                            toDownload[index] = e.IsChecked;
                        });

                    //builder.SetSingleChoiceItems(Item.ToArray(), -1, (object sender, DialogClickEventArgs e) =>
                    //    {
                    //        if ((int)e.Which == 0)
                    //        {
                    //            btn_pay.Text = "Okay";
                    //            //					StartActivityFromFragment(this.FragmentManager.GetFragment(), takePicture, 0);
                    //            //					alertDialog.Dismiss();
                    //        }
                    //        else if ((int)e.Which == -1)
                    //        {
                    //            btn_pay.Text = "NotOkay";
                    //            //					StartActivityFromFragment(this.FragmentManager.GetFragment(), pickPhoto, 1);
                    //            //					alertDialog.Dismiss();
                    //        }
                    //    })
                        builder.SetNegativeButton("Отмена", delegate
                        {
                            //Some to do...
                        })
                        .SetPositiveButton("Открыть", delegate
                        {
                            if (toDownload[0] == true)
                            {
                                //to do...
                            }
                            if (s_lock_unlock_door.Text == "заблокирована")
                                s_lock_unlock_door.Text = "разблокирована";
                            else
                                s_lock_unlock_door.Text = "заблокирована";
                        });
                        
                        alertDialog = builder.Create();
                        alertDialog.Show();
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                    }
                            
            };

            btn_pay.Click += async delegate
            {
                if(s_payment.Text != "Оплачено")
                {
                    progressBar.Progress = 8;
                    status_view.Text = "8. Завершение использования";

                    s_pin_access_code.Text = "1324";
                    s_payment.Text = "Оплачено";
                    Toast.MakeText(this, "Оплата произведена", ToastLength.Long).Show();
                    GetInfoAboutBox(dir_path);
                    
                }
                else
                {
                    Toast.MakeText(this, "Оплата уже была произведена", ToastLength.Long).Show();
                }
                
            };

            btn_pass_delivery_service.Click += async delegate
            {
                Android.Support.V7.App.AlertDialog alertDialog;
                List<string> Item = new List<string>();
                Item.Add("Выгрузка завершена. Контейнер готов к отправке.");


                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetTitle("Вы действительно хотите закрыть замок контейнера?");

                bool[] toDownload = { false };
                builder.SetMultiChoiceItems(Item.ToArray(), toDownload, (sender, e) =>
                {
                    int index = e.Which;

                    toDownload[index] = e.IsChecked;
                });

                builder.SetNegativeButton("Отмена", delegate
                {
                    //Some to do...
                })
                .SetPositiveButton("Закрыть", delegate
                {
                    if (toDownload[0] == true)
                    {
                                //to do...
                            }
                    if (s_lock_unlock_door.Text == "заблокирована")
                        s_lock_unlock_door.Text = "разблокирована";
                    else
                        s_lock_unlock_door.Text = "заблокирована";
                });

                alertDialog = builder.Create();
                alertDialog.Show();
            };

            btn_exit_.Click += async delegate
            {
                File.Delete(dir_path + "user_data.txt");
                ClearField();
                Intent ActivityMain = new Intent(this, typeof(MainActivity));
                StartActivity(ActivityMain);
            };

            

        }

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

                string name = container.Name;//!!!!
                
              
                //if(s_payment.Text == "Оплачено")
                //{
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
                            //container_name.Text = exported_data.Name.ToString();
                            //container_name.Text = name;

                            s_temperature.Text = exported_data.Temperature;
                            s_light.Text = exported_data.Light.ToString();
                            s_humidity.Text = exported_data.Wetness;
                            StaticBox.Longitude = exported_data.Longitude;
                            StaticBox.Latitude = exported_data.Latitude;
                            s_longitude.Text = StaticBox.Longitude.ToString();
                            s_latitude.Text = StaticBox.Latitude.ToString();
                            //coordinates lat lon
                            s_weight.Text = "100.0";
                            //progressBar.Progress = 6;
                        
                        Text3.Text = "ПИН-код доступа отобразится после оплаты";

                        //status_view.Text = "6. Ожидание выгрузки";
                        if (s_payment.Text == "Оплачено")
                            s_pin_access_code.Text = (exported_data.Code == null) ? "0000" : "1324";// !!!!  
                        else
                            s_pin_access_code.Text = "****";
                            if (exported_data.IsOpenedDoor.ToString() == "true")
                            {
                                s_lock_unlock_door.Text = "разблокирована";
                            }
                            else
                            {
                                s_lock_unlock_door.Text = "заблокирована";
                            }
                            s_cost.Text = "39537.5";


                            //var boxState = s_open_close_container.Text;
                            //var doorState = s_lock_unlock_door.Text;

                            
                        }
                        else
                        {
                            Toast.MakeText(this, o_data.Message, ToastLength.Long).Show();
                        }
                    }
                
                //else if(s_payment.Text == "Не оплачено")
                //{
                //    s_temperature.Text = "****";
                //    s_light.Text = "****";
                //    s_humidity.Text = "****";
                //    s_weight.Text = "****";
                //    s_pin_access_code.Text = "****";
                //    s_lock_unlock_door.Text = "****";
                //    s_cost.Text = "1000";
                //    container_name.Text = name;
                //    s_latitude.Text = "****";
                //    s_longitude.Text = "****";
                //}
                
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
            locationCallback = new AuthLocationCallBack(this);
        }

        private void BuildLocationRequest()
        {
            locationRequest = new LocationRequest();
            locationRequest.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);
            locationRequest.SetInterval(1000);
            locationRequest.SetFastestInterval(3000);
            locationRequest.SetSmallestDisplacement(10f);
        }


        internal class AuthLocationCallBack : LocationCallback // !!!!
        {
            private ActivityUserBox activityUserBoxy;

            public AuthLocationCallBack(ActivityUserBox activityUserBoxy)
            {
                this.activityUserBoxy = activityUserBoxy;
            }

            public override async void OnLocationResult(LocationResult result)
            {
                base.OnLocationResult(result);

            //    StaticBox.Latitude = result.LastLocation.Latitude;
            //    StaticBox.Longitude = result.LastLocation.Longitude;

            //    s_longitude.Text = result.LastLocation.Latitude.ToString();
            //    s_latitude.Text = result.LastLocation.Longitude.ToString();

            //    BoxLocation gpsLocation = new BoxLocation
            //    {
            //        id = StaticBox.SmartBoxId,
            //        lat1 = StaticBox.Latitude,
            //        lon1 = StaticBox.Longitude,                   
            //    };



            //    var myHttpClient = new HttpClient();
            //    var uri = new Uri("http://iot-tmc-cen.1gb.ru/api/container/setcontainerlocation?id=" + gpsLocation.id + "&lat1=" + gpsLocation.lat1 + "&lon1=" + gpsLocation.lon1);
            //    //json структура.
            //    var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            //{
            //    { "Id", gpsLocation.id },
            //    { "Lon1", gpsLocation.lon1.ToString()},
            //    { "Lat1", gpsLocation.lat1.ToString()},              
            //});

            //    HttpResponseMessage response = await myHttpClient.PostAsync(uri.ToString(), formContent);
            //    AuthApiData<BaseResponseObject> o_data = new AuthApiData<BaseResponseObject>();

            //    string s_result;
            //    using (HttpContent responseContent = response.Content)
            //    {
            //        s_result = await responseContent.ReadAsStringAsync();
            //    }

            //    o_data = JsonConvert.DeserializeObject<AuthApiData<BaseResponseObject>>(s_result);
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