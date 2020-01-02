using Android.App;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V7.App;
using GeoGeometry.Activity.Cameraa;
using Android.Gms.Common.Apis;
using Java.Lang;
using Android.Gms.Common;
using Android.Content;
using Android.Util;
using Android.Runtime;
using System.Threading.Tasks;
using Java.IO;
using Android.Gms.Drive;
using System;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityCamera")]
    public class ActivityCamera : AppCompatActivity //GoogleApiClient.IConnectionCallbacks, IResultCallback, IDriveApiDriveContentsResult
    {
        const string TAG = "GDriveExample";
        const int REQUEST_CODE_RESOLUTION = 3;
        internal const string ExtraDriveIds = "extra_driveids";
        internal const string ExtraDumpDbPath = "extra_dumppath";
        string[] driveIds;
        string dumpPath;

        GoogleApiClient _googleApiClient;

        //public void OnConnected(Bundle connectionHint)
        //{
        //    Log.Info(TAG, "Client connected.");
        //    DriveClass.DriveApi.NewDriveContents(_googleApiClient).SetResultCallback(this);
        //}

        //public void OnConnectionSuspended(int cause)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void OnResult(Java.Lang.Object result)
        //{
        //    throw new System.NotImplementedException();
        //}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.camera_activity);
            //////////////
            //if (_googleApiClient == null)
            //{
            //    _googleApiClient = new GoogleApiClient.Builder(this)
            //      .AddApi(DriveClass.API)
            //      .AddScope(DriveClass.ScopeFile)
            //      .AddConnectionCallbacks(this)
            //      .AddOnConnectionFailedListener(onConnectionFailed)
            //      .Build();
            //}
            //if (!_googleApiClient.IsConnected)
            //    _googleApiClient.Connect();
            //////////////
            FragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, new CameraFragment())
               .Commit();

            
        }

        //protected void onConnectionFailed(ConnectionResult result)
        //{
        //    Log.Info(TAG, "GoogleApiClient connection failed: " + result);
        //    if (!result.HasResolution)
        //    {
        //        GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0).Show();
        //        return;
        //    }
        //    try
        //    {
        //        result.StartResolutionForResult(this, REQUEST_CODE_RESOLUTION);
        //    }
        //    catch (IntentSender.SendIntentException e)
        //    {
        //        Log.Error(TAG, "Exception while starting resolution activity", e);
        //    }
        //}

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
            
        //    if (requestCode == REQUEST_CODE_RESOLUTION)
        //    {
        //        switch (resultCode)
        //        {
        //            case Result.Ok:
        //                _googleApiClient.Connect();
        //                break;
        //            case Result.Canceled:
        //                Log.Error(TAG, "Unable to sign in, is app registered for Drive access in Google Dev Console?"); // !!!
        //                break;
        //            case Result.FirstUser:
        //                Log.Error(TAG, "Unable to sign in: RESULT_FIRST_USER");
        //                break;
        //            default:
        //                Log.Error(TAG, "Should never be here: " + resultCode);
        //                return;
        //        }
        //    }
        //}

        //void IResultCallback.OnResult(Java.Lang.Object result)
        //{
        //    var contentResults = (result).JavaCast<IDriveApiDriveContentsResult>();
        //    if (!contentResults.Status.IsSuccess) // handle the error
        //        return;
        //    Task.Run(() =>
        //    {
        //        var writer = new OutputStreamWriter(contentResults.DriveContents.OutputStream);
        //        writer.Write("Stack Overflow");
        //        writer.Close();
        //        MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
        //               .SetTitle("New Text File")
        //               .SetMimeType("text/plain")
        //               .Build();

        //        DriveClass.DriveApi
        //                  .GetRootFolder(_googleApiClient)
        //                  .CreateFile(_googleApiClient, changeSet, contentResults.DriveContents);
        //    });
        //}

        //public IDriveContents DriveContents
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public Statuses Status
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    }
}