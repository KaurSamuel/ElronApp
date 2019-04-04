﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SharpTrooper.Core;
using SharpTrooper.Entities;
using Xamarin.Essentials;

namespace StarWarsApi
{
    [Activity(Label = "ItemActivity")]
    public class ItemActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailsLayout);
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("No internet connection");
            alert.SetMessage("You need internet connection to use this app");

            alert.SetNegativeButton("OK", (senderAlert, args) => { });

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
            string option = Intent.GetSerializableExtra("OptionName").ToString();
            string itemName = Intent.GetSerializableExtra("ItemName").ToString();
            string url=Intent.GetSerializableExtra("ItemUrl").ToString();

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = itemName;

            SharpTrooperCore core = new SharpTrooperCore();
            SharpEntity itemInfo;
            ItemStrings itemStrings = new ItemStrings();
            SortedDictionary<string, object> itemDic = new SortedDictionary<string, object>();

            using (UserDialogs.Instance.Loading())
            {
                switch (option)
                {
                    case ("Planets"):
                        itemInfo = await core.GetSingleByUrl<Planet>(url);

                        itemDic = DictionaryFromType(itemInfo);

                        break;
                    case ("People"):
                        itemInfo = await core.GetSingleByUrl<People>(url);

                        itemDic = DictionaryFromType(itemInfo);

                        break;
                    case ("Films"):
                        itemInfo = await core.GetSingleByUrl<Film>(url);

                        itemDic = DictionaryFromType(itemInfo);
                        
                        break;
                    case ("Species"):
                        itemInfo = await core.GetSingleByUrl<Specie>(url);

                        itemDic = DictionaryFromType(itemInfo);
                        
                        break;
                    case ("StarShips"):
                        itemInfo = await core.GetSingleByUrl<Starship>(url);

                        itemDic = DictionaryFromType(itemInfo);
                        
                        break;
                    case ("Vehicles"):
                        itemInfo = await core.GetSingleByUrl<Vehicle>(url);

                        itemDic = DictionaryFromType(itemInfo);
                        
                        break;
                };

                foreach (var item in itemDic)
                {
                    string itemKey = item.Key;
                    if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url" || itemKey == "title" || itemKey == "vehicle_class")
                        continue;
                    itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                    itemStrings.PropertyNames.Add(itemKey);

                    if (item.Value == null)
                    {
                        itemStrings.PropertyValues.Add("unknown");
                    }
                    else if (item.Value.GetType() == typeof(List<string>))
                    {
                        List<string> stringList = new List<string>();

                        foreach (var listItem in (List<string>)item.Value)
                        {
                            string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                            category = char.ToUpper(category[0]) + category.Substring(1);
                            switch (category)
                            {
                                case ("Planets"):
                                    Planet planetResult = await core.GetSingleByUrl<Planet>(listItem);
                                    stringList.Add(planetResult.name);
                                    break;
                                case ("People"):
                                    People peopleResult = await core.GetSingleByUrl<People>(listItem);
                                    stringList.Add(peopleResult.name);
                                    break;
                                case ("Films"):
                                    Film filmResult = await core.GetSingleByUrl<Film>(listItem);
                                    stringList.Add(filmResult.title);
                                    break;
                                case ("Species"):
                                    Specie speciesResult = await core.GetSingleByUrl<Specie>(listItem);
                                    stringList.Add(speciesResult.name);
                                    break;
                                case ("Starships"):
                                    Starship starshipResult = await core.GetSingleByUrl<Starship>(listItem);
                                    stringList.Add(starshipResult.name);
                                    break;
                                case ("Vehicles"):
                                    Vehicle vehicleResult = await core.GetSingleByUrl<Vehicle>(listItem);
                                    stringList.Add(vehicleResult.name);
                                    break;
                            }
                        }
                        string listToString = string.Join(", ", stringList);
                        itemStrings.PropertyValues.Add(listToString);
                    }
                    else if (item.Value.ToString().StartsWith("https://swapi.co/api/"))
                    {
                        string category = item.Value.ToString().Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                        category = char.ToUpper(category[0]) + category.Substring(1);
                        switch (category)
                        {
                            case ("Planets"):
                                Planet planetResult = await core.GetSingleByUrl<Planet>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(planetResult.name);
                                break;
                            case ("People"):
                                People peopleResult = await core.GetSingleByUrl<People>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(peopleResult.name);
                                break;
                            case ("Films"):
                                Film filmResult = await core.GetSingleByUrl<Film>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(filmResult.title);
                                break;
                            case ("Species"):
                                Specie speciesResult = await core.GetSingleByUrl<Specie>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(speciesResult.name);
                                break;
                            case ("Starships"):
                                Starship starshipResult = await core.GetSingleByUrl<Starship>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(starshipResult.name);
                                break;
                            case ("Vehicles"):
                                Vehicle vehicleResult = await core.GetSingleByUrl<Vehicle>(item.Value.ToString());
                                itemStrings.PropertyValues.Add(vehicleResult.name);
                                break;
                        }
                    }
                    else
                    {
                        itemStrings.PropertyValues.Add(item.Value.ToString());
                    }
                }
            }

            ListView listview = FindViewById<ListView>(Resource.Id.listView_Item);
            listview.Adapter = new DetailsAdapter(this, itemStrings.PropertyNames, itemStrings.PropertyValues);
        }

        public static SortedDictionary<string, object> DictionaryFromType(object atype)
        {
            if (atype == null) return new SortedDictionary<string, object>();
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            SortedDictionary<string, object> dict = new SortedDictionary<string, object>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });
                dict.Add(prp.Name, value);
            }
            return dict;
        }
    }
}