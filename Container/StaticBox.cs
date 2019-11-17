using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GeoGeometry.Model;
using GeoGeometry.Model.Box;
using GeoGeometry.Model.User;

namespace GeoGeometry.Container
{
    class StaticBox
    {
        /// <summary>
        /// Id контейнера.
        /// </summary>
       static public string SmartBoxId;

        /// <summary>
        /// Разложен ли контейнер.
        /// </summary>
        static public bool IsOpenedBox { get; set; }

        /// <summary>
        /// Открыта ли дверь.
        /// </summary>
        static public bool IsOpenedDoor { get; set; }

        /// <summary>
        /// Освещённость.
        /// </summary>
        static public int Light { get; set; }

        /// <summary>
        /// Пин-код для открытия двери.
        /// </summary>
        static public string Code { get; set; }

        /// <summary>
        /// Температура.
        /// </summary>
        static public double Temperature { get; set; }

        /// <summary>
        /// Влажность.
        /// </summary>
        static public double Wetness { get; set; }

        /// <summary>
        /// Заряд батареи.
        /// </summary>
        static public double BatteryPower { get; set; }

        /// <summary>
        /// Наименование контейнера.
        /// </summary>
        static public string Name { get; set; }

        /// <summary>
        /// Вес контейнера.
        /// </summary>
        static public double Weight { get; set; }

        static public List<ContainerResponse> Objects { get; set; }

        /// <summary>
        /// Добавляю информацию о клиенте
        /// </summary>
        /// <param name="o_auth">Объект авторизации/регистрации</param>
        public static void AddInfoAuth(ContainerResponse o_auth)
        {
            SmartBoxId = o_auth.SmartBoxId;
            Name = o_auth.Name;
        }

        public static void AddInfoObjects(ListResponse<ContainerResponse> boxes)
        {
            Objects = new List<ContainerResponse>();
            foreach (var box in boxes.Objects)
            {
                Objects.Add(box);
            }
        }

        //крч здесь заполнишь все значения сам
        public static void AddInfoBox(BoxDataResponse boxData)
        {
            SmartBoxId = boxData.Id;
            IsOpenedDoor = boxData.IsOpenedBox;
            Weight = boxData.Weight;
            Light = boxData.Light;
            Code = boxData.Code;
            Temperature = boxData.Temperature;
            Wetness = boxData.Wetness;
            BatteryPower = boxData.BatteryPower;
        }
    }
}


        /*

        /// <summary>
        /// Добавляю информацию о клиенте
        /// </summary>
        /// <param name="o_auth">Объект настройки</param>
        public static void AddInfoUserSettings(UserSettingsResponseData o_user_settings)
        {
            PhoneNumber = o_user_settings.PhoneNumber;
            Email = o_user_settings.Email;
            IsEmailConfirmed = o_user_settings.IsEmailConfirmed;
            IsLeader = o_user_settings.IsLeader;
        }

        /// <summary>
        /// Добавляю информацию о клиенте
        /// </summary>
        /// <param name="o_auth">Объект клиент</param>
        public static void AddInfoUser(UserResponseData o_user)
        {
            MiddleName = o_user.MiddleName;
            Team = o_user.Team;
            TeamId = o_user.TeamId;
            Section = o_user.Section;
            Rang = o_user.Rang;
            Imagines = o_user.Imagines;
        }
        */
 