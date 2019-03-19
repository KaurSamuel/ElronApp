using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;

namespace StarWarsApi
{
    //class DetailsAdapter : BaseAdapter<Dictionary<string,object>>
    //{
    //    Dictionary<string, object> items;
    //    Activity context;


    //    public DetailsAdapter(Activity context, Dictionary<string,object> items) : base()
    //    {
    //        this.context = context;
    //        this.items = items;
    //    }

    //    public override Dictionary<string, object> this[string key]
    //    {
    //        get { return items[key]; }
    //    }

    //    //public override string this[int position]
    //    //{
    //    //    get { return items[position]; }
    //    //}// => throw new System.NotImplementedException();

    //    //public override Dictionary<string, object> this[string key, object value]
    //    //{
    //    //    get { return items.ToDictionary(v => v, v => true)[key, value]; }
    //    //}

    //    public override int Count
    //    {
    //        get
    //        {
    //            return items.Count;
    //        }
    //    }

    //    public override long GetItemId(int position)
    //    {
    //        return position;
    //    }

    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        var view = convertView;
    //        if (view == null)
    //            view = context.LayoutInflater.Inflate(Resource.Layout.details_row, null);

    //        view.FindViewById<TextView>(Resource.Id.textView_Key).Text = items[position].key;
    //        view.FindViewById<TextView>(Resource.Id.textView_Value).Text = items[position].WeatherDescription;

    //        return view;
    //    }
    //}
}