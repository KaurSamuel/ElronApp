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
using SharpTrooper.Core;

namespace StarWarsApi
{
    [Activity(Label = "OptionListActivity")]
    public class OptionListActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_layout);

            SharpTrooperCore core = new SharpTrooperCore();

            List<string> OptionList = new List<string>();

            string option = Intent.GetSerializableExtra("ButtonName").ToString();

            switch (option)
            {
                case ("Planets"):
                    var planetList = core.GetAllPlanets().results;

                    foreach (var planet in planetList)
                    {
                        OptionList.Add(planet.name);
                    };
                    break;
                case ("People"):
                    var peopleList = core.GetAllPeople().results;

                    foreach (var person in peopleList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
                case ("Films"):
                    var filmList = core.GetAllPeople().results;

                    foreach (var person in filmList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
                case ("Species"):
                    var speciesList = core.GetAllPeople().results;

                    foreach (var person in speciesList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
                case ("Starships"):
                    var starshipList = core.GetAllPeople().results;

                    foreach (var person in starshipList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
                case ("Vehicles"):
                    var vehicleList = core.GetAllPeople().results;

                    foreach (var person in vehicleList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
            };

            ListView listview = FindViewById<ListView>(Resource.Id.listView_selectedOption);
            listview.Adapter = new CustomAdapter(this, OptionList);

            //var listView = FindViewById<ListView>(Resource.Id.listView_selectedOption);
            //ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            //  {
            //      Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            //  };

            // Create your application here
        }
    }
}