//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Telephony;
//using Android.Views;
//using Android.Widget;

//namespace GeoGeometry.Container
//{
//    class SignalStrength : PhoneStateListener
//    {
//        public delegate void SignalStrengthChangedDelegate(int strength);

//        public event SignalStrengthChangedDelegate SignalStrengthChanged;

//        public override void OnSignalStrengthsChanged(SignalStrength newSignalStrength)
//        {
//            if (newSignalStrength.IsGsm)
//            {
//                if (SignalStrengthChanged != null)
//                {
//                    SignalStrengthChanged(newSignalStrength.GsmSignalStrength);
//                }
//            }
//        }
//    }
//}