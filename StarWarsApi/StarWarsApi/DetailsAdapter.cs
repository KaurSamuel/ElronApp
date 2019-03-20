using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;

namespace StarWarsApi
{
    class DetailsAdapter : BaseAdapter<string>
    {
        ItemStrings itemLists=new ItemStrings();
        Activity context;


        public DetailsAdapter(Activity context, List<string> propertyNames, List<string> propertyValues) : base()
        {
            this.context = context;
            itemLists.PropertyNames = propertyNames;
            itemLists.PropertyValues = propertyValues;
        }

        public override string this[int position] 
        {
            get { return itemLists.PropertyNames[position]; }
        }

        public override int Count
        {
            get
            {
                return itemLists.PropertyNames.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.details_row, null);

            view.FindViewById<TextView>(Resource.Id.textView_Key).Text = itemLists.PropertyNames[position];
            view.FindViewById<TextView>(Resource.Id.textView_Value).Text = itemLists.PropertyValues[position];

            return view;
        }
    }
}