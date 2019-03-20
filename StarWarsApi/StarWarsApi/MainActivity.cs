using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;
using Acr.UserDialogs;

namespace StarWarsApi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button buttonPlanets;
        Button buttonSpecies;
        Button buttonPeople;
        Button buttonStarships;
        Button buttonFilms;
        Button buttonVehicles;
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

            #region Button definitions
            buttonPlanets = FindViewById<Button>(Resource.Id.Planets);
            buttonPeople = FindViewById<Button>(Resource.Id.People);
            buttonFilms = FindViewById<Button>(Resource.Id.Films);
            buttonSpecies = FindViewById<Button>(Resource.Id.Species);
            buttonStarships = FindViewById<Button>(Resource.Id.StarShips);
            buttonVehicles = FindViewById<Button>(Resource.Id.Vehicles);
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
            using (UserDialogs.Instance.Loading("wait..."))
            {
                buttonName = buttonPlanets.Text;
                var ListActivity = new Intent(this, typeof(OptionListActivity));
                ListActivity.PutExtra("ButtonName", buttonName);
                StartActivity(ListActivity);
            }
        }

        public void ButtonPeople_Click(object sender, EventArgs e)
        {
            buttonName = buttonPeople.Text;
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonFilms_Click(object sender, EventArgs e)
        {
            buttonName = buttonFilms.Text;
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonSpecies_Click(object sender, EventArgs e)
        {
            buttonName = buttonSpecies.Text;
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonStarships_Click(object sender, EventArgs e)
        {
            buttonName = buttonStarships.Text;
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }

        public void ButtonVehicles_Click(object sender, EventArgs e)
        {
            buttonName = buttonVehicles.Text;
            var ListActivity = new Intent(this, typeof(OptionListActivity));
            ListActivity.PutExtra("ButtonName", buttonName);
            StartActivity(ListActivity);
        }
        #endregion
    }
}