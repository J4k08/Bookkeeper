using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace BookkeeperLabb2
{
	public class EntryAdapter : BaseAdapter
	{

		private Activity context;
		public List<Entry> Entries { get; set; }
		public EntryAdapter(Activity activity, List<Entry> entries)
		{
			this.context = activity;
			Entries = entries;
		}

		public override int Count
		{
			get
			{
				return Entries.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return Entries[position];	
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = context.LayoutInflater.Inflate(Resource.Layout.EntryListItem, parent, false);

			view.FindViewById<TextView>(Resource.Id.);
		}
	}
}
