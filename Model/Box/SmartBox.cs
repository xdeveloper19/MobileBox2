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

namespace GeoGeometry.Model.Box
{
    public class SmartBox
    {
        public enum ContainerState
        {
            //сложенный, то есть контейнер закрыт

            onBase = 1, //на складе
            onCar,//на автомобиле
            onShipper, //выгружен у грузоотправителя
            onConsignee//разгружен у грузополучателя
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOpenedBox { get; set; }
        public ContainerState BoxState { get; set; }
        public bool IsOpenedDoor { get; set; }


        public double Weight { get; set; }


        public int Light { get; set; }
        public string Code { get; set; }


        public double Temperature { get; set; }


        public double Wetness { get; set; }
        public double BatteryPower { get; set; }
    }
}