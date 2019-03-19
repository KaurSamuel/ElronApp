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
            string itemString = Intent.GetSerializableExtra("ItemName").ToString();
            string url=Intent.GetSerializableExtra("ItemUrl").ToString();

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = itemString;

            List<string> itemList = new List<string>();
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
                            string listToString = "";

                            foreach (var item1 in (List<string>)item.Value)
                            {
                                listToString = item1.ToString();
                            }
                            itemStrings.PropertyValues.Add(listToString);
                        }
                        else if (item.Value.ToString().StartsWith("https://swapi.co/api/"))
                        {

                        }
                        else
                        {
                            itemStrings.PropertyValues.Add(item.Value.ToString());
                        }
                    }
                    break;
                case ("People"):
                    itemInfo = core.GetSingleByUrl<People>(url);

                    break;
                case ("Films"):
                    itemInfo = core.GetSingleByUrl<Film>(url);
                    break;
                case ("Species"):
                    itemInfo = core.GetSingleByUrl<Specie>(url);
                    break;
                case ("StarShips"):
                    itemInfo = core.GetSingleByUrl<Starship>(url);
                    break;
                case ("Vehicles"):
                    itemInfo = core.GetSingleByUrl<Vehicle>(url);
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