using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SQLite;

namespace BookkeeperLabb2
{
	public class EntryAdapter : BaseAdapter
	{

		private Activity context;
		SQLiteConnection db;

		public EntryAdapter(Activity activity)
		{
			this.context = activity;
		}

		public override int Count
		{
			get
			{
				return Entries.Count;
			}
		}
		public List<Entry> Entries 
		{
			get
			{
				return BookKeeperManager.Instance.getEntries();
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
			View view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.EntryListItem, parent, false);


			view.FindViewById<TextView>(Resource.Id.ListItem_date).Text = Entries[position].Date.ToString("yyyy-MM-dd");
			view.FindViewById<TextView>(Resource.Id.ListItem_description).Text = Entries[position].Description.ToString();
			view.FindViewById<TextView>(Resource.Id.ListItem_amount).Text = Entries[position].Amount+" kr";

			return view;
		}
	}
}
