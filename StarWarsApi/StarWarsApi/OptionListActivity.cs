using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
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
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_layout);

            SharpTrooperCore core = new SharpTrooperCore();

            List<string> nameList = new List<string>();
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            string option = Intent.GetSerializableExtra("ButtonName").ToString();
            switch (option)
            {
                case ("Planets"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            SharpEntityResults<Planet> Data;
                            Data = await core.GetAllPlanets(i.ToString());

                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.name, item.url);
                            }
                        }
                    }
                    break;
                case ("People"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 9; i++)
                        {
                            SharpEntityResults<People> Data;
                            using (UserDialogs.Instance.Loading())
                            {
                                Data = await core.GetAllPeople(i.ToString());
                            }
                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.name, item.url);
                            }
                        }
                    }
                    break;
                case ("Films"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 2; i++)
                        {
                            SharpEntityResults<Film> Data;
                            using (UserDialogs.Instance.Loading())
                            {
                                Data = await core.GetAllFilms(i.ToString());
                            }
                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.title, item.url);
                            }
                        }
                    }
                    break;
                case ("Species"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            SharpEntityResults<Specie> Data;
                            using (UserDialogs.Instance.Loading())
                            {
                                Data = await core.GetAllSpecies(i.ToString());
                            }
                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.name, item.url);
                            }
                        }
                    }
                    break;
                case ("StarShips"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            SharpEntityResults<Starship> Data;
                            using (UserDialogs.Instance.Loading())
                            {
                                Data = await core.GetAllStarships(i.ToString());
                            }
                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.name, item.url);
                            }
                        }
                    }
                    break;
                case ("Vehicles"):
                    using (UserDialogs.Instance.Loading())
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            SharpEntityResults<Vehicle> Data;
                            using (UserDialogs.Instance.Loading())
                            {
                                Data = await core.GetAllVehicles(i.ToString());
                            }
                            foreach (var item in Data.results)
                            {
                                valuePairs.Add(item.name, item.url);
                            }
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
                  ItemActivity.PutExtra("ItemUrl", url);
                  StartActivity(ItemActivity);
              };
        }
    }
}