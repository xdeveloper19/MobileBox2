using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;


namespace GeoGeometry.Activity.Cameraa
{
    public class CameraPictureCallBack : Java.Lang.Object, Android.Hardware.Camera.IPictureCallback
    {
        const string APP_NAME = "GeoGeometry";
        Context _context;



        public CameraPictureCallBack(Context cont)//1
        {
            _context = cont;
        }

        /// <summary>
        /// Callback when the picture is taken by the Camera
        /// </summary>
        /// <param name="data"></param>
        /// <param name="camera"></param>
        public void OnPictureTaken(byte[] data, Android.Hardware.Camera camera)//2
        {
            try
            {
                string fileName = Uri.Parse("test.jpg").LastPathSegment;
                var os = _context.OpenFileOutput(fileName, FileCreationMode.Private);
                System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(os);
                binaryWriter.Write(data);
                binaryWriter.Close();

                //We start the camera preview back since after taking a picture it freezes
                camera.StartPreview();
            }
            catch (System.Exception e)
            {
                Log.Debug(APP_NAME, "File not found: " + e.Message);
            }
        }
    }
}