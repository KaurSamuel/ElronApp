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

            List<string> nameList = new List<string>();
            List<SharpEntity> _optionList = new List<SharpEntity>();
            //List<string> nameList = new List<string>();
            List<string> list = new List<string>();
            string option = Intent.GetSerializableExtra("ButtonName").ToString();

            switch (option)
            {


                case ("Planets"):

                    for (int i = 1; i < 7; i++)
                    {
                        var planets = core.GetAllPlanets(i.ToString()).results;
                        foreach (var item in planets)
                        {
                            nameList.Add(item.name);
                        }
                    }


                    //case ("Planets"):
                    //    var count=core.GetAllPlanets().count;
                    //    for (int i = 2; i <= count; i++)
                    //    {
                    //        _optionList.Add(core.GetPlanet(i.ToString()));
                    //    }
                    //    foreach (Planet item in _optionList)
                    //    {
                    //        nameList.Add(item.name);
                    //    }

                    //var planetList = core.GetAllPlanets().results;

                    //foreach (var planet in planetList)
                    //{
                    //    OptionList.Add(planet.name);
                    //};
                    break;
                case ("People"):
                    var count = core.GetAllPeople().count;
                    for (int i = 2; i <= count; i++)
                    {
                        _optionList.Add(core.GetPeople(i.ToString()));
                    }
                    foreach (People item in _optionList)
                    {
                        if (item != null)
                            nameList.Add(item.name);
                    }
                    break;
                case ("Films"):
                    count = core.GetAllFilms().count;
                    for (int i = 1; i <= count; i++)
                    {
                        _optionList.Add(core.GetFilm(i.ToString()));
                    }
                    foreach (Film item in _optionList)
                    {
                        if (item != null)
                            nameList.Add(item.title);
                    }
                    break;
                case ("Species"):
                    count = core.GetAllSpecies().count;
                    for (int i = 1; i <= count; i++)
                    {
                        _optionList.Add(core.GetSpecie(i.ToString()));
                    }
                    foreach (Specie item in _optionList)
                    {
                        if (item != null)
                            nameList.Add(item.name);
                    }
                    break;
                case ("StarShips"):
                    count = core.GetAllStarships().count;
                    for (int i = 2; i <= count; i++)
                    {
                        _optionList.Add(core.GetStarship(i.ToString()));
                    }
                    foreach (Starship item in _optionList)
                    {
                        if (item != null)
                            nameList.Add(item.name);
                    }
                    break;
                case ("Vehicles"):
                    count = core.GetAllVehicles().count;
                    for (int i = 1; i <= count; i++)
                    {
                        _optionList.Add(core.GetVehicle(i.ToString()));
                    }
                    foreach (Vehicle item in _optionList)
                    {
                        if (item != null)
                            nameList.Add(item.name);
                    }
                    break;
            };
            foreach (var item in list)
            {
                nameList.Add(item);
            }
            
            ListView listview = FindViewById<ListView>(Resource.Id.listView_selectedOption);
            listview.Adapter = new CustomAdapter(this, nameList);

            listview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
              {
                  var item = listview.Adapter.GetItem(args.Position).ToString();
                  int index = nameList.FindIndex(x => x == item);
                  var ItemActivity = new Intent(this, typeof(ItemActivity));
                  ItemActivity.PutExtra("OptionName", option);
                  ItemActivity.PutExtra("ItemName", item);
                  ItemActivity.PutExtra("ItemIndex", index);
                  //ItemActivity.PutExtra("ItemList", OptionList);
                  StartActivity(ItemActivity);
              };
            // Create your application here
        }
    }
}