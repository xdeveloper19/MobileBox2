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
    public class DriverMenuFragment: Android.App.Fragment
    {
        /// <summary>
        /// Конпка "Страницы Список заказов".
        /// </summary>
        private AppCompatImageButton btn_list_orders;

        /// <summary>
        /// Конпка "Страницы Список задач".
        /// </summary>
        private AppCompatImageButton btn_tasks;

        /// <summary>
        /// Конпка "Страницы Список контейнеров".
        /// </summary>
        private AppCompatImageButton btn_boxes;

        /// <summary>
        /// Конпка "Страницы Текущий заказ".
        /// </summary>
        private AppCompatImageButton btn_driver_page;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View main_menu = inflater.Inflate(Resource.Layout.fragment_menu_driver, container, false);

            if (StaticMenu.id_page != 6)
            {
                StaticMenu.id_page = 6;
                // Переход к Отряды.
                btn_list_orders = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_orders);
                btn_list_orders.Click += (s, e) =>
                {
                    Intent squadActivity = new Intent(Activity, typeof(Auth.ActivityOrderList));
                    StartActivity(squadActivity);
                };
            }

            if (StaticMenu.id_page != 5)
            {
                StaticMenu.id_page = 5;
                // Переход к Главная.
                btn_tasks = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_tasks); 
                btn_tasks.Click += (s, e) =>
                {
                    Intent squadActivity = new Intent(Activity, typeof(Auth.ActivityOrderListDriver));
                    StartActivity(squadActivity);
                };
            }

            if (StaticMenu.id_page != 7)
            {
                StaticMenu.id_page = 7;
                // Переход к Камерe.
                btn_boxes = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_boxes);
                btn_boxes.Click += (s, e) =>
                {

                    //if (CrossMedia.Current.IsCameraAvailable)   // камера готова к работе
                    //{
                    //  const string permission = Manifest.Permission.Camera;
                    // if (Context.CheckSelfPermission(permission) == (int)Permission.Granted)
                    //{

                    Intent orderActivity = new Intent(Activity, typeof(Auth.ContainerSelection));
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

                if (StaticMenu.id_page != 8)
                {
                    StaticMenu.id_page = 8;
                    // Переход к Клиент.
                    btn_driver_page = main_menu.FindViewById<AppCompatImageButton>(Resource.Id.btn_driver_page);
                    btn_driver_page.Click += (s, e) =>
                    {
                        Intent userActivity = new Intent(Activity, typeof(Auth.DriverActivity));
                        StartActivity(userActivity);
                    };

                }
            }
            return main_menu;
        }
    }
}