using Android.App;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V7.App;
using GeoGeometry.Activity.Cameraa;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityCamera")]
    public class ActivityCamera : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.camera_activity);

            FragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, new CameraFragment())
               .Commit();
        }
    }
}