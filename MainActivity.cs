using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Single;
using Com.Karumi.Dexter.Listener;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using GeoGeometry.Model.Auth;

namespace GeoGeometry.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        RelativeLayout main_form;

        /// <summary>
        /// Конпка прехода на форму авторизации.
        /// </summary>
        private Button btn_auth_form;

        /// <summary>
        /// Конпка прехода на форму регистрации.
        /// </summary>
        private Button btn_reg_form;

       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            string dir_path = "/storage/emulated/0/Android/data/GeoGeometry.GeoGeometry/files/";
            string file_data_remember;
            //using (FileStream file = new FileStream(dir_path + "user_data.txt", FileMode.Open, FileAccess.Read))
            //{
            //    // преобразуем строку в байты
            //    byte[] array = new byte[file.Length];
            //    // считываем данные
            //    file.Read(array, 0, array.Length);
            //    // декодируем байты в строку
            //    file_data_remember = Encoding.Default.GetString(array);
            //    file.Close();
            //}

           //AuthResponseData user = JsonConvert.DeserializeObject<AuthResponseData>(file_data_remember);
           // if (user.Check == "1")
           // {
           //     if (user.RoleName == "driver")
           //     {
           //         Intent Driver = new Intent(this, typeof(Auth.DriverActivity));
           //         StartActivity(Driver);
           //         this.Finish();

           //     }
           //     else if (user.RoleName == "user")
           //     {
           //         Intent UserActivity = new Intent(this, typeof(Auth.ActivityUserBox));
           //         StartActivity(UserActivity);
           //         this.Finish();
           //     }
           // }
           
            btn_auth_form = FindViewById<Button>(Resource.Id.btn_auth_form);
            btn_reg_form = FindViewById<Button>(Resource.Id.btn_reg_form);

            Dexter.WithActivity(this)
                .WithPermission(Manifest.Permission.AccessFineLocation)
                .WithListener(new CompositePermissionListener(new SamplePermissionListener(this))).Check();

                // Переход к форме регистрации.
            btn_reg_form.Click += (s, e) =>
            {
                Intent registerActivity = new Intent(this, typeof(Auth.RegisterActivity));
                StartActivity(registerActivity);
            };

                // Переход к форме авторизация
            
            btn_auth_form.Click += (s, e) =>
            {
                Intent authActivity = new Intent(this, typeof(Auth.AuthActivity));
                StartActivity(authActivity);
            };
            
        }

        private class SamplePermissionListener : Java.Lang.Object, IPermissionListener
        {
            MainActivity activity;
            public SamplePermissionListener(MainActivity activity)
            {
                this.activity = activity;
            }

            public void OnPermissionDenied(PermissionDeniedResponse p0)
            {
                //Snackbar.Make(activity.main_form, "Permission Denied", Snackbar.LengthShort).Show();
            }

            public void OnPermissionGranted(PermissionGrantedResponse p0)
            {
                //Snackbar.Make(activity.main_form, "Permission Granted", Snackbar.LengthShort).Show();
            }

            public void OnPermissionRationaleShouldBeShown(PermissionRequest p0, IPermissionToken token)
            {
                activity.ShowRequestPermissionRationale(token);
            }
        }

        private void ShowRequestPermissionRationale(IPermissionToken token)
        {
            new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle("Разрешения GPS")
                .SetMessage("Необходимо разрешить использовать ваши gps данные")
                .SetNegativeButton("Отмена", delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetPositiveButton("Ok", delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetOnDismissListener(new MyDismissListener(token)).Show();
        }
    }

    internal class MyDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
    {
        private IPermissionToken token;

        public MyDismissListener(IPermissionToken token)
        {
            this.token = token;
        }

        public void OnDismiss(IDialogInterface dialog)
        {
            token.CancelPermissionRequest();
        }
    }
}