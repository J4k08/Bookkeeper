
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
using System.Globalization;

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
		bool income = true;

		RadioButton rbIncome;
		RadioButton rbExpense;

		TextView tvDate;
		Button btnDate;
		Button btnAddEntry;

		EditText etDescription;
		EditText etAmount;
		EditText etAmountExclTax;
		DateTime dateTime;

		private Spinner spMoneyAccount;
		private Spinner spAccount;
		private Spinner spTax;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			rbIncome = FindViewById<RadioButton>(Resource.Id.RB_income);
			rbExpense = FindViewById<RadioButton>(Resource.Id.RB_expense);

			tvDate = FindViewById<TextView>(Resource.Id.BTN_date);
			btnDate = FindViewById<Button>(Resource.Id.BTN_date);
			btnDate.Click += DateSelect;


			etDescription = FindViewById<EditText>(Resource.Id.ET_description);
			etAmount = FindViewById<EditText>(Resource.Id.ET_amount);

			btnAddEntry = FindViewById<Button>(Resource.Id.BTN_addEntry);


			setAdapters(income);


			btnAddEntry.Click += delegate
			{
				setEntryValues();
				Entry e = new Entry
				{
					Income = income,
					Description = description,
					Date = dateTime,
					AccountId = moneyAccount,
					TypeId = type,
					TaxRateId = tax
				};
				BookKeeperManager.Instance.AddEntry(e);
			};

			rbIncome.Click += delegate
			{
				if (rbIncome.Checked)
				{
					Console.WriteLine(income = true);
					setTypeSpinner(income);
				}
			};

			rbExpense.Click += delegate
			{
				if (rbExpense.Checked)
				{
					Console.WriteLine(income = false);
					setTypeSpinner(income);
				}

			};

		}
		void DateSelect(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
																		 tvDate.Text = time.ToLongDateString();
																	 });
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}

		private void setAdapters(bool b)
		{
			setMoneyAccountSpinner();
			setTypeSpinner(income);
			setTaxRateSpinner();

		}

		private void setMoneyAccountSpinner()
		{
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
														BookKeeperManager.Instance.getAccounts("moneyAccount"));
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spMoneyAccount.Adapter = typeAdapter;
		}

		private void setTypeSpinner(bool income)
		{
			if (income)
			{
				ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
															BookKeeperManager.Instance.getAccounts("income"));
				typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				spAccount.Adapter = typeAdapter;
			}
			else
			{
				ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
															BookKeeperManager.Instance.getAccounts("expense"));
				typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				spAccount.Adapter = typeAdapter;
			}
		}
		private void setTaxRateSpinner()
		{
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
														BookKeeperManager.Instance.getTaxRates());
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spTax.Adapter = typeAdapter;
		}

		private void setEntryValues()
		{
			description = etDescription.Text;
			amount = Int32.Parse(etAmount.Text);

			moneyAccount = ((Account)spMoneyAccount.SelectedItem).Number;
			tax = taxRate.Tax;


			if (income)
			{
				ac = BookKeeperManager.Instance.getAccounts("income")[spAccount.SelectedItemPosition];
			}
			else
			{
				ac = BookKeeperManager.Instance.getAccounts("expense")[spAccount.SelectedItemPosition];
			}

			type = ((Account)spAccount.SelectedItem).Number;

		}

	}
}