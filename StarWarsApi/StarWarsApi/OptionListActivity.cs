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
using SharpTrooper.Entities;

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
            List<SharpEntity> _optionList = new List<SharpEntity>();
            List<string> nameList = new List<string>();

            string option = Intent.GetSerializableExtra("ButtonName").ToString();

            switch (option)
            {
                case ("Planets"):
                    for (int i = 1; i <= 61; i++)
                    {
                        var item=core.GetPlanet(i.ToString());
                        _optionList.Add(item);
                    }

                    //var planetList = core.GetAllPlanets().results;

                    //foreach (var planet in planetList)
                    //{
                    //    OptionList.Add(planet.name);
                    //};
                    break;
                case ("People"):
                    var peopleList = core.GetAllPeople().results;

                    foreach (var person in peopleList)
                    {
                        OptionList.Add(person.name);
                    }
                    break;
                case ("Films"):
                    var filmList = core.GetAllFilms().results;

                    foreach (var film in filmList)
                    {
                        OptionList.Add(film.title);
                    }
                    break;
                case ("Species"):
                    var speciesList = core.GetAllSpecies().results;

                    foreach (var species in speciesList)
                    {
                        OptionList.Add(species.name);
                    }
                    break;
                case ("StarShips"):
                    var starshipList = core.GetAllStarships().results;

                    foreach (var starship in starshipList)
                    {
                        OptionList.Add(starship.name);
                    }
                    break;
                case ("Vehicles"):
                    var vehicleList = core.GetAllVehicles().results;

                    foreach (var vehicle in vehicleList)
                    {
                        OptionList.Add(vehicle.name);
                    }
                    break;
            };


            foreach (Planet item in _optionList)
            {
                nameList.Add(item.name);
            }
            ListView listview = FindViewById<ListView>(Resource.Id.listView_selectedOption);
            listview.Adapter = new CustomAdapter(this, nameList);

            listview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
              {
                  var item = listview.Adapter.GetItem(args.Position).ToString();
                  var ItemActivity = new Intent(this, typeof(ItemActivity));
                  ItemActivity.PutExtra("OptionName", option);
                  ItemActivity.PutExtra("ItemName", item);
                  StartActivity(ItemActivity);
              };
            // Create your application here
        }
    }
}