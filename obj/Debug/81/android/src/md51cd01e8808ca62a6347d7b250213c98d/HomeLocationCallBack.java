package md51cd01e8808ca62a6347d7b250213c98d;


public class HomeLocationCallBack
	extends md57dae306e9c511046bb3e5da82eb8f47a.LocationCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLocationResult:(Lcom/google/android/gms/location/LocationResult;)V:GetOnLocationResult_Lcom_google_android_gms_location_LocationResult_Handler\n" +
			"";
		mono.android.Runtime.register ("GeoGeometry.Activity.Home.HomeLocationCallBack, GeoGeometry", HomeLocationCallBack.class, __md_methods);
	}


	public HomeLocationCallBack ()
	{
		super ();
		if (getClass () == HomeLocationCallBack.class)
			mono.android.TypeManager.Activate ("GeoGeometry.Activity.Home.HomeLocationCallBack, GeoGeometry", "", this, new java.lang.Object[] {  });
	}


	public void onLocationResult (com.google.android.gms.location.LocationResult p0)
	{
		n_onLocationResult (p0);
	}

	private native void n_onLocationResult (com.google.android.gms.location.LocationResult p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
