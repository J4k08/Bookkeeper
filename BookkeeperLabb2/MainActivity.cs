using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace BookkeeperLabb2
{
	[Activity(Label = "c#Labb2", MainLauncher = true)]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			Button btn_newEntry =  FindViewById<Button>(Resource.Id.BTN_createEntry);

			btn_newEntry.Click += delegate
			{
				Intent i = new Intent(this, typeof(NewEntryActivity));
				StartActivity(i);

			};
		}
	}
}

