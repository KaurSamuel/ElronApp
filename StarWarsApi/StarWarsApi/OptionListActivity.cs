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
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            List<string> urlList = new List<string>();
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
                            valuePairs.Add(item.name, item.url);
                            //nameList.Add(item.name);
                            //urlList.Add(item.url);
                        }
                    }
                    #region Old Code
                    //nice code
                    //for (int i = 1; i < 7; i++)
                    //{
                    //    var planets = core.GetAllPlanets(i.ToString()).results;
                    //    foreach (var item in planets)
                    //    {
                    //        nameList.Add(item.name);
                    //        urlList.Add(item.url);
                    //    }
                    //}


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
                    #endregion

                    break;
                case ("People"):
                    for (int i = 1; i < 9; i++)
                    {
                        var Data = core.GetAllPeople(i.ToString()).results;
                        foreach (var item in Data)
                        {
                            valuePairs.Add(item.name, item.url);
                        }
                    }
                    break;
                case ("Films"):
                    for (int i = 1; i < 1; i++)
                    {
                        var Data = core.GetAllFilms(i.ToString()).results;
                        foreach (var item in Data)
                        {
                            valuePairs.Add(item.title, item.url);
                        }
                    }
                    break;
                case ("Species"):
                    for (int i = 1; i < 4; i++)
                    {
                        var Data = core.GetAllSpecies(i.ToString()).results;
                        foreach (var item in Data)
                        {
                            valuePairs.Add(item.name, item.url);
                        }
                    }
                    break;
                case ("StarShips"):
                    for (int i = 1; i < 4; i++)
                    {
                        var Data = core.GetAllSpecies(i.ToString()).results;
                        foreach (var item in Data)
                        {
                            valuePairs.Add(item.name, item.url);
                        }
                    }
                    break;
                case ("Vehicles"):
                    for (int i = 1; i < 4; i++)
                    {
                        var Data = core.GetAllVehicles(i.ToString()).results;
                        foreach (var item in Data)
                        {
                            valuePairs.Add(item.name, item.url);
                        }
                    }
                    break;
            };
            foreach (KeyValuePair<string, string> kvp in valuePairs)
            {
                nameList.Add(kvp.Key);
            }
            
            ListView listview = FindViewById<ListView>(Resource.Id.listView_selectedOption);
            listview.Adapter = new CustomAdapter(this, nameList);

            listview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
              {
                  var item = listview.Adapter.GetItem(args.Position).ToString();
                  var url = valuePairs[item];
                  
                  int index = nameList.FindIndex(x => x == item);
                  var ItemActivity = new Intent(this, typeof(ItemActivity));
                  ItemActivity.PutExtra("OptionName", option);
                  ItemActivity.PutExtra("ItemName", item);
                  ItemActivity.PutExtra("ItemIndex", index);
                  ItemActivity.PutExtra("ItemUrl", url);
                  //ItemActivity.PutExtra("ItemList", OptionList);
                  StartActivity(ItemActivity);
              };
            // Create your application here
        }
    }
}