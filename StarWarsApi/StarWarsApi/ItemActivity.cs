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
            int index = int.Parse(Intent.GetSerializableExtra("ItemIndex").ToString());
            string url=Intent.GetSerializableExtra("ItemUrl").ToString();
            //index++;

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = itemString;

            List<string> itemList = new List<string>();
            SharpTrooperCore core = new SharpTrooperCore();
            SharpEntity item;

            switch (option)
            {
                case ("Planets"):
                    item = core.GetSingleByUrl<Planet>(url);
                    var itemDic=DictionaryFromType(item);
                    break;
                case ("People"):
                    item = core.GetSingleByUrl<People>(url);
                    break;
                case ("Films"):
                    item = core.GetSingleByUrl<Film>(url);
                    break;
                case ("Species"):
                    item = core.GetSingleByUrl<Specie>(url);
                    break;
                case ("StarShips"):
                    item = core.GetSingleByUrl<Starship>(url);
                    break;
                case ("Vehicles"):
                    item = core.GetSingleByUrl<Vehicle>(url);
                    break;
            };

            //index = 0;


        }

        /// code from https://stackoverflow.com/a/737159
        /// <summary>
        /// creates a dictionary with an object's property name as a key and the respective property's value as a value  
        /// </summary>
        public static Dictionary<string, object> DictionaryFromType(object atype)
        {
            if (atype == null) return new Dictionary<string, object>();
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });
                dict.Add(prp.Name, value);
            }
            return dict;
        }
    }
}