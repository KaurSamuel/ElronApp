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

namespace StarWarsApi
{
    class People
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; } 
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string BirthYear { get; set; }
        public string Gender { get; set; }
        public string Homeworld { get; set; }

        public string[] Films { get; set; }
        public string[] Vehicles { get; set; }
        public string[] Starships { get; set; }
    }
}