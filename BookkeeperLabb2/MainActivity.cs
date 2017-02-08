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
			Button btn_viewEntry = FindViewById<Button>(Resource.Id.BTN_viewEntry);
			Button btn_createTaxReport = FindViewById<Button>(Resource.Id.BTN_createReport);

			btn_newEntry.Click += delegate
			{
				Intent newEntryIntent = new Intent(this, typeof(NewEntryActivity));
				StartActivity(newEntryIntent);

			};

			btn_viewEntry.Click += delegate
			{
				Intent viewEntryIntent = new Intent(this, typeof(ViewEntryActivity));
				StartActivity(viewEntryIntent);
			};

			btn_createTaxReport.Click += delegate
			{
				Intent createReportIntent = new Intent(this, typeof(CreateReportsActivity));
				StartActivity(createReportIntent);
			};

		}
	}
}

