using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace GeoGeometry.Activity.Auth
{
    [Activity(Label = "ActivityApplication")]
    class ActivityApplication : AppCompatActivity
    {
        #region Переменные

        private EditText s_edit_from;

        private EditText s_edit_where;

        private EditText s_shipment_time;

        private EditText s_shipping_date;

        private EditText s_length;

        private EditText s_width;

        private EditText s_height;

        private EditText s_size;

        private EditText s_weight;

        private EditText s_sum_seats;

        private EditText s_declared_value_goods;

        private EditText s_order_cost;

        private EditText s_contact_person;

        private EditText s_phone;

        private EditText s_email_notifications;

        private EditText s_comment_order;

        private Spinner s_cargo_characteristic;

        private Spinner s_hazard_class;

        private Spinner s_loading_methods;

        private RadioButton r_cargo_insurance;

        private string a_cargo_characteristic;

        private string a_hazard_class;

        private string a_loading_methodsc;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_lol);

            Button btn_make_request = FindViewById<Button>(Resource.Id.btn_make_request);
            s_edit_from = FindViewById<EditText>(Resource.Id.s_edit_from);
            s_edit_where = FindViewById<EditText>(Resource.Id.s_edit_where);
            s_shipment_time = FindViewById<EditText>(Resource.Id.s_shipment_time);
            s_shipping_date = FindViewById<EditText>(Resource.Id.s_shipping_date);
            s_length = FindViewById<EditText>(Resource.Id.s_length);
            s_width = FindViewById<EditText>(Resource.Id.s_width);
            s_weight = FindViewById<EditText>(Resource.Id.s_weight);
            s_height = FindViewById<EditText>(Resource.Id.s_height);
            s_size = FindViewById<EditText>(Resource.Id.s_size);
            s_sum_seats = FindViewById<EditText>(Resource.Id.s_sum_seats);
            s_order_cost = FindViewById<EditText>(Resource.Id.s_order_cost);
            s_contact_person = FindViewById<EditText>(Resource.Id.s_contact_person);
            s_phone = FindViewById<EditText>(Resource.Id.s_phone);
            s_email_notifications = FindViewById<EditText>(Resource.Id.s_email_notifications);
            s_comment_order = FindViewById<EditText>(Resource.Id.s_comment_order);
            s_cargo_characteristic = FindViewById<Spinner>(Resource.Id.s_cargo_characteristic);
            s_hazard_class = FindViewById<Spinner>(Resource.Id.s_hazard_class);
            s_loading_methods = FindViewById<Spinner>(Resource.Id.s_loading_methods);
            r_cargo_insurance = FindViewById<RadioButton>(Resource.Id.r_cargo_insurance);

            ProgressBar preloader = FindViewById<ProgressBar>(Resource.Id.preloader);

            r_cargo_insurance.Click += RadioButtonClick;

            s_cargo_characteristic.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(this, Resource.Array.array_cargo_characteristic, Android.Resource.Layout.SimpleSpinnerItem);

            s_hazard_class.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.array_hazard_class, Android.Resource.Layout.SimpleSpinnerItem);

            s_loading_methods.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(this, Resource.Array.array_loading_methodsc, Android.Resource.Layout.SimpleSpinnerItem);


            btn_make_request.Click += async delegate
            {
                Toast.MakeText(this, "Заявка оформлена", ToastLength.Long).Show();
                Intent ActivityUserBox = new Intent(this, typeof(ActivityUserBox));
                StartActivity(ActivityUserBox);
            };

        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            a_cargo_characteristic = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
        }


    }
}