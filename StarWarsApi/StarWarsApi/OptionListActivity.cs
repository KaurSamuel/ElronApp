using System.Collections.Generic;
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
    [Activity(Label = "OptionListActivity")]
    public class OptionListActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_layout);
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("No internet connection");
            alert.SetMessage("You need internet connection to use this app");

            alert.SetNegativeButton("OK", (senderAlert, args) => { });

            Dialog dialog = alert.Create();

            var MainActivity = new Intent(this, typeof(MainActivity));

            List<string> nameList = new List<string>();
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            string option = Intent.GetSerializableExtra("ButtonName").ToString();

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
                    SharpTrooperCore core = new SharpTrooperCore();

                    using (UserDialogs.Instance.Loading())
                    {
                        switch (option)
                        {
                            case ("Planets"):
                                for (int i = 1; i < 7; i++)
                                {
                                    SharpEntityResults<Planet> Data;
                                    Data = await core.GetAllPlanets(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.name, item.url);
                                    }
                                }
                                break;
                            case ("People"):
                                for (int i = 1; i < 9; i++)
                                {
                                    SharpEntityResults<People> Data;
                                    Data = await core.GetAllPeople(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.name, item.url);
                                    }
                                }
                                break;
                            case ("Films"):
                                for (int i = 1; i < 2; i++)
                                {
                                    SharpEntityResults<Film> Data;
                                    Data = await core.GetAllFilms(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.title, item.url);
                                    }
                                }
                                break;
                            case ("Species"):
                                for (int i = 1; i < 4; i++)
                                {
                                    SharpEntityResults<Specie> Data;
                                    Data = await core.GetAllSpecies(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.name, item.url);
                                    }
                                }
                                break;
                            case ("StarShips"):
                                for (int i = 1; i < 4; i++)
                                {
                                    SharpEntityResults<Starship> Data;
                                    Data = await core.GetAllStarships(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.name, item.url);
                                    }
                                }
                                break;
                            case ("Vehicles"):
                                
                                for (int i = 1; i < 4; i++)
                                {
                                    SharpEntityResults<Vehicle> Data;
                                    Data = await core.GetAllVehicles(i.ToString());
                                    foreach (var item in Data.results)
                                    {
                                        valuePairs.Add(item.name, item.url);
                                    }
                                }
                                break;
                        }
                        break;
                    }
            }

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

        public override void OnBackPressed()
        {
            Finish();
            StartActivity(typeof(MainActivity));
        }
    }
}
