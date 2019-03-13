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
            string item = Intent.GetSerializableExtra("ItemName").ToString();

            TextView nameBig = FindViewById<TextView>(Resource.Id.textView_nameBig);
            nameBig.Text = item;

            List<string> itemList = new List<string>();
            SharpTrooperCore core = new SharpTrooperCore();
            core.GetSingleByUrl<SharpTrooper.Entities.SharpEntity>("swapi.co/api/"+option.ToLower()+"/"+item);

            // Create your application here
        }
    }
}