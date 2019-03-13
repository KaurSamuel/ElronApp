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
        string buttonName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //dynamic x = ApiSecvice.Get_Single("people/");

            buttonPlanets = FindViewById<Button>(Resource.Id.Planets);
            buttonPeople = FindViewById<Button>(Resource.Id.People);
            buttonFilms = FindViewById<Button>(Resource.Id.Films);
            buttonSpecies = FindViewById<Button>(Resource.Id.Species);
            buttonStarships = FindViewById<Button>(Resource.Id.StarShips);
            buttonVehicles = FindViewById<Button>(Resource.Id.Vehicles);
            imgplanets = FindViewById<ImageView>(Resource.Id.imageView_Planets);
            imgpeople = FindViewById<ImageView>(Resource.Id.imageView_People);

            buttonPlanets.Click += ButtonPlanets_Click;
            buttonPeople.Click += ButtonPeople_Click;
            buttonFilms.Click += ButtonFilms_Click;
            buttonSpecies.Click += ButtonSpecies_Click;
            buttonStarships.Click += ButtonStarships_Click;
            buttonVehicles.Click += ButtonVehicles_Click;
            imgplanets.Click += ButtonPlanets_Click;
            imgpeople.Click += ButtonPeople_Click;
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