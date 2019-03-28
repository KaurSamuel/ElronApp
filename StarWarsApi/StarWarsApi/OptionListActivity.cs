﻿using System;
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
using Xamarin.Essentials;

namespace StarWarsApi
{
    [Activity(Label = "OptionListActivity")]
    public class OptionListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_layout);
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("No internet connection");
            alert.SetMessage("You need internet connection to use this app");

            alert.SetNegativeButton("OK", (senderAlert, args) => { });

            Dialog dialog = alert.Create();

            var MainActivity = new Intent(this, typeof(MainActivity));
            

            var current = Connectivity.NetworkAccess;
            switch (current)
            {
                case NetworkAccess.Unknown:
                    dialog.Show();
                    StartActivity(MainActivity);
                    break;
                case NetworkAccess.None:
                    dialog.Show();
                    StartActivity(MainActivity);
                    break;
                case NetworkAccess.Local:
                    dialog.Show();
                    StartActivity(MainActivity);
                    break;
                case NetworkAccess.ConstrainedInternet:
                    dialog.Show();
                    StartActivity(MainActivity);
                    break;
                case NetworkAccess.Internet:
                    {
                        SharpTrooperCore core = new SharpTrooperCore();

                        List<string> nameList = new List<string>();
                        Dictionary<string, string> valuePairs = new Dictionary<string, string>();
                        string option = Intent.GetSerializableExtra("ButtonName").ToString();

                        switch (option)
                        {
                            case ("Planets"):
                                for (int i = 1; i < 7; i++)
                                {
                                    var Data = core.GetAllPlanets(i.ToString()).results;
                                    foreach (var item in Data)
                                    {
                                        valuePairs.Add(item.name, item.url);

                                    }
                                }
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
                                for (int i = 1; i < 2; i++)
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
                                    var Data = core.GetAllStarships(i.ToString()).results;
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
                            ItemActivity.PutExtra("ItemUrl", url);
                            StartActivity(ItemActivity);
                        };
                    }
                    break;
                default:
                    break;
            }
          
        }
    }
}