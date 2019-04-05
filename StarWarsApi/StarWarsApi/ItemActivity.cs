using System;
using System.Collections.Generic;
using System.Linq;
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

                itemStrings = await core.GetStringListsOfItemDictionary(itemDic);
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