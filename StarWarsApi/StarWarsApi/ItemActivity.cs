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
            //index++;

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = itemString;

            List<string> itemList = new List<string>();
            SharpTrooperCore core = new SharpTrooperCore();
            //var result=core.GetSingleByUrl<People>("https://swapi.co/api" + "/" +option.ToLower() + "/" + index);
            SharpEntity item;

            switch (option)
            {
                case ("Planets"):
                    item = core.GetSingleByUrl<Planet>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    foreach (var  in collection)
                    {

                    }
                    break;
                case ("People"):
                    item = core.GetSingleByUrl<People>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    break;
                case ("Films"):
                    item = core.GetSingleByUrl<Film>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    break;
                case ("Species"):
                    item = core.GetSingleByUrl<Specie>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    break;
                case ("StarShips"):
                    item = core.GetSingleByUrl<Starship>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    break;
                case ("Vehicles"):
                    item = core.GetSingleByUrl<Vehicle>("https://swapi.co/api" + "/" + option.ToLower() + "/" + index);
                    break;
            };

            //index = 0;
        }
    }
}