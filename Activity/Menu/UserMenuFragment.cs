using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace GeoGeometry.Activity.Menu
{
    public class UserMenuFragment: Android.App.Fragment
    {
        /// <summary>
        /// Конпка "Страницы Список заказов".
        /// </summary>
        private AppCompatImageButton btn_list_orders;

        /// <summary>
        /// Конпка "Страницы Камера".
        /// </summary>
        private AppCompatImageButton btn_camera1;

        /// <summary>
        /// Конпка "Страницы Новый заказ".
        /// </summary>
        private AppCompatImageButton btn_add_order;

        /// <summary>
        /// Конпка "Страницы Текущий заказ".
        /// </summary>
        private AppCompatImageButton btn_user_page;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View main_menu = inflater.Inflate(Resource.Layout.fragment_menu_user, container, false);

            if (StaticMenu.id_page != 2)
            {
                StaticMenu.id_page = 2;
                // Переход к Отряды.
                btn_list_orders = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_list_orders);
                btn_list_orders.Click += (s, e) =>
                {
                    Intent squadActivity = new Intent(Activity, typeof(Auth.ContainerSelection));
                    StartActivity(squadActivity);
                };
            }

            if (StaticMenu.id_page != 1)
            {
                StaticMenu.id_page = 1;
                // Переход к Главная.
                btn_camera1 = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_camera1);
                btn_camera1.Click += (s, e) =>
                {
                    try
                    {
                        Intent ActivityC = new Intent(Activity, typeof(Auth.ActivityCamera));
                        StartActivity(ActivityC);
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(Activity, "" + ex.Message, ToastLength.Long).Show();
                    }
                };
            }

            if (StaticMenu.id_page != 3)
            {
                StaticMenu.id_page = 3;
                // Переход к Камерe.
                btn_add_order = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_add_order);
                btn_add_order.Click += (s, e) =>
                {

                    //if (CrossMedia.Current.IsCameraAvailable)   // камера готова к работе
                    //{
                    //  const string permission = Manifest.Permission.Camera;
                    // if (Context.CheckSelfPermission(permission) == (int)Permission.Granted)
                    //{

                    Intent orderActivity = new Intent(Activity, typeof(Auth.ActivityApplication));
                    StartActivity(orderActivity);
                    //}//
                    //else
                    //{
                    //  string[] PermissionsCamera = { Manifest.Permission.Camera };
                    // int RequestCameraId = 0;

                    // Snackbar.Make(btn_camera, "Для работы приложения необходимо разрешение на использование камеры", Snackbar.LengthIndefinite)
                    //           .SetAction("OK", v => RequestPermissions(PermissionsCamera, RequestCameraId)).Show();
                    //}//
                    //};
                };

                if (StaticMenu.id_page != 4)
                {
                    StaticMenu.id_page = 4;
                    // Переход к Клиент.
                    btn_user_page = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_user_page);
                    btn_user_page.Click += (s, e) =>
                    {
                        Intent userActivity = new Intent(Activity, typeof(Auth.ActivityUserBox));
                        StartActivity(userActivity);
                    };

                }
            }
            return main_menu;
        }
    }
}