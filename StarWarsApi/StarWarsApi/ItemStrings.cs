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
    public class ItemStrings
    {
        public List<string> PropertyNames { get; set; }
        public List<string> PropertyValues { get; set; }

        public ItemStrings()
        {
            PropertyNames = new List<string>();
            PropertyValues = new List<string>();
        }
    }
}