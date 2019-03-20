using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    [Activity(Label = "ItemActivity")]
    public class ItemActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailsLayout);

            string option = Intent.GetSerializableExtra("OptionName").ToString();
            string itemName = Intent.GetSerializableExtra("ItemName").ToString();
            string url=Intent.GetSerializableExtra("ItemUrl").ToString();

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = itemName;

            SharpTrooperCore core = new SharpTrooperCore();
            SharpEntity itemInfo;
            ItemStrings itemStrings = new ItemStrings();

            switch (option)
            {
                case ("Planets"):
                    itemInfo = core.GetSingleByUrl<Planet>(url);
                    var itemDic=DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name"|| itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);
                        
                        if(item.Value.GetType()==typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("People"):
                    itemInfo = core.GetSingleByUrl<People>(url);

                    itemDic = DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);

                        if (item.Value.GetType() == typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("Films"):
                    itemInfo = core.GetSingleByUrl<Film>(url);

                    itemDic = DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);

                        if (item.Value.GetType() == typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("Species"):
                    itemInfo = core.GetSingleByUrl<Specie>(url);

                    itemDic = DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);

                        if (item.Value.GetType() == typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("StarShips"):
                    itemInfo = core.GetSingleByUrl<Starship>(url);

                    itemDic = DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);

                        if (item.Value.GetType() == typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("Vehicles"):
                    itemInfo = core.GetSingleByUrl<Vehicle>(url);

                    itemDic = DictionaryFromType(itemInfo);

                    foreach (var item in itemDic)
                    {
                        string itemKey = item.Key;
                        if (itemKey == "name" || itemKey == "created" || itemKey == "edited" || itemKey == "url")
                            continue;
                        itemKey = char.ToUpper(itemKey[0]) + itemKey.Substring(1).Replace("_", " ");
                        itemStrings.PropertyNames.Add(itemKey);

                        if (item.Value.GetType() == typeof(List<string>))
                        {
                            List<string> stringList = new List<string>();

                            foreach (var listItem in (List<string>)item.Value)
                            {
                                string category = listItem.Substring(21).TrimEnd('/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                                category = char.ToUpper(category[0]) + category.Substring(1);
                                switch (category)
                                {
                                    case ("Planets"):
                                        Planet planetResult = core.GetSingleByUrl<Planet>(listItem);
                                        stringList.Add(planetResult.name);
                                        break;
                                    case ("People"):
                                        People peopleResult = core.GetSingleByUrl<People>(listItem);
                                        stringList.Add(peopleResult.name);
                                        break;
                                    case ("Films"):
                                        Film filmResult = core.GetSingleByUrl<Film>(listItem);
                                        stringList.Add(filmResult.title);
                                        break;
                                    case ("Species"):
                                        Specie speciesResult = core.GetSingleByUrl<Specie>(listItem);
                                        stringList.Add(speciesResult.name);
                                        break;
                                    case ("Starships"):
                                        Starship starshipResult = core.GetSingleByUrl<Starship>(listItem);
                                        stringList.Add(starshipResult.name);
                                        break;
                                    case ("Vehicles"):
                                        Vehicle vehicleResult = core.GetSingleByUrl<Vehicle>(listItem);
                                        stringList.Add(vehicleResult.name);
                                        break;
                                }
                            }
                            string listToString = string.Join(", ", stringList);
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
            };

            ListView listview = FindViewById<ListView>(Resource.Id.listView_Item);
            listview.Adapter = new DetailsAdapter(this, itemStrings.PropertyNames, itemStrings.PropertyValues);
        }

        /// code from https://stackoverflow.com/a/737159
        /// <summary>
        /// creates a dictionary with an object's property name as a key and the respective property's value as a value  
        /// </summary>
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