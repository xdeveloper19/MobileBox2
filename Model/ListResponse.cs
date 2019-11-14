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
using GeoGeometry.Model.Auth;

namespace GeoGeometry.Model
{
    //ответ с сервера. 
    public class ListResponse<T> : BaseResponseObject
    {
        public ListResponse()
        {
            this.Objects = new List<T>();
        }
        public List<T> Objects { get; set; }
    }
}