using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using Android.Content;
using Acr.UserDialogs;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Xamarin.Essentials;

namespace StarWarsApi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        ImageView imgplanets;
        ImageView imgpeople;
        ImageView imgspecies;
        ImageView imgstarships;
        ImageView imgfilms;
        ImageView imgvehicles;
        string buttonName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AppCenter.Start("013a6849-359c-489a-88db-974cce2c1722",
                   typeof(Analytics), typeof(Crashes),typeof(Distribute));

            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("No internet connection");
            alert.SetMessage("You need internet connection to use this app");

            alert.SetNegativeButton("OK", (senderAlert, args) => {});

            Dialog dialog = alert.Create();

            var current = Connectivity.NetworkAccess;
            switch (current)
            {
                case NetworkAccess.Unknown:
                    dialog.Show();
                    break;
                case NetworkAccess.None:
                    dialog.Show();
                    break;
                case NetworkAccess.Local:
                    dialog.Show();
                    break;
                case NetworkAccess.ConstrainedInternet:
                    dialog.Show();
                    break;
                case NetworkAccess.Internet:
                    break;
                default:
                    break;
            }

            #region Button definitions
            imgplanets = FindViewById<ImageView>(Resource.Id.imageView_Planets);
            imgpeople = FindViewById<ImageView>(Resource.Id.imageView_People);
            imgspecies = FindViewById<ImageView>(Resource.Id.imageView_Species);
            imgstarships = FindViewById<ImageView>(Resource.Id.imageView_Starships);
            imgfilms = FindViewById<ImageView>(Resource.Id.imageView_Films);
            imgvehicles = FindViewById<ImageView>(Resource.Id.imageView_Vehicles);
            #endregion

            #region Click events
            imgplanets.Click += ButtonPlanets_Click;
            imgpeople.Click += ButtonPeople_Click;
            imgfilms.Click += ButtonFilms_Click;
            imgspecies.Click += ButtonSpecies_Click;
            imgstarships.Click += ButtonStarships_Click;
            imgvehicles.Click += ButtonVehicles_Click;
            imgplanets.Click += ButtonPlanets_Click;
            imgpeople.Click += ButtonPeople_Click;
            #endregion

            UserDialogs.Init(this);
        }

        #region Buttons

        public void ButtonPlanets_Click(object sender, EventArgs e)
        {
            buttonName = "Planets";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonPeople_Click(object sender, EventArgs e)
        {
            buttonName = "People";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonFilms_Click(object sender, EventArgs e)
        {
            buttonName = "Films";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonSpecies_Click(object sender, EventArgs e)
        {
            buttonName = "Species";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonStarships_Click(object sender, EventArgs e)
        {
            buttonName = "StarShips";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonVehicles_Click(object sender, EventArgs e)
        {
            buttonName = "Vehicles";
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }
        #endregion
    }
}