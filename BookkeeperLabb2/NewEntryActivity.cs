
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

namespace BookkeeperLabb2
{
	[Activity(Label = "NewEntryActivity")]
	public class NewEntryActivity : Activity
	{

		Account ac;
		TaxRate taxRate;
		Account moneyAc;

		int amount;
		int type;
		int moneyAccount;
		double tax;
		string description = "";

		RadioButton rbIncome;
		RadioButton rbExpense;

		TextView dateDisplay;
		Button btnDate;
		bool income = true;

		Button addEntry;

		EditText etDescription;
		EditText etAmmunt;
		EditText etAmountExclTax;
		DateTime dateTime;

		Spinner spMoneyAccount;
		Spinner spAccount;
		Spinner spTax;




		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			rbIncome = FindViewById<RadioButton>(Resource.Id.)


			rbIncome.Click += delegate
			{
				if (rbIncome.Checked)
				{
					Console.WriteLine(income = true);
					//setAdapter(income);
				}
			};

			rbExpense.Click += delegate
			{
				if (rbExpense.Checked)
				{
					Console.WriteLine(income = false);
					//setAdapter(income);
				}

		}
	}
}
