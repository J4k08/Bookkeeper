
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

		private Account ac;
		//TaxRate taxRate;
		//Account moneyAc;
		private string amountChanged;
		private int amount;
		private int type;
		private int moneyAccount;
		private double tax;
		private double tempTax;
		private string description = "";
		private bool income = true;

		private RadioButton rbIncome;
		private RadioButton rbExpense;

		private TextView tvAmountExlTax;
		private Button btnDate;
		private Button btnAddEntry;

		private EditText etDescription;
		private EditText etAmount;

		private DateTime dateTime;

		private Spinner spMoneyAccount;
		private Spinner spAccount;
		private Spinner spTax;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.NewEntryLayout);

			rbIncome = FindViewById<RadioButton>(Resource.Id.RB_income);
			rbExpense = FindViewById<RadioButton>(Resource.Id.RB_expense);

			btnDate = FindViewById<Button>(Resource.Id.BTN_date);
			btnDate.Click += DateSelect;

			spAccount = FindViewById<Spinner>(Resource.Id.SP_type);
			spTax = FindViewById<Spinner>(Resource.Id.SP_taxRate);
			spMoneyAccount = FindViewById<Spinner>(Resource.Id.SP_moneyAccount);

			etDescription = FindViewById<EditText>(Resource.Id.ET_description);
			etAmount = FindViewById<EditText>(Resource.Id.ET_amount);

			tvAmountExlTax = FindViewById<TextView>(Resource.Id.TV_totalAmountExclTax);

			btnAddEntry = FindViewById<Button>(Resource.Id.BTN_addEntry);


			setAdapters(income);


			etAmount.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
			{
				amountChanged = e.Text.ToString();
				calculateTaxFree(amountChanged);

			};


			btnAddEntry.Click += delegate
			{
				setEntryValues();
				Entry e = new Entry
				{
					isIncome = income,
					Description = description,
					Date = dateTime,
					Amount = amount,
					AccountMoney = moneyAccount,
					AccountType = type,
					TaxRate = tax
				};
				BookKeeperManager.Instance.AddEntry(e);
				Toast.MakeText(this, "Händelse skapad!", ToastLength.Short).Show();

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

			spTax.ItemSelected += delegate {
				
				tempTax = ((TaxRate)spTax.SelectedItem).Tax;
				calculateTaxFree(amountChanged);
				
			};

		}

		void DateSelect(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
				btnDate.Text = time.ToString("yy-MM-dd");
				dateTime = time;
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
			amount = Int32.Parse(etAmount.Text.ToString());

			moneyAccount = ((Account)spMoneyAccount.SelectedItem).Number;


			tax = ((TaxRate)spTax.SelectedItem).Tax;

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

		private void calculateTaxFree(string s)
		{
			int num;
			tempTax = ((TaxRate)spTax.SelectedItem).Tax;
			if (Int32.TryParse(etAmount.Text.ToString(), out num))
			{
				tvAmountExlTax.Text = "" + (num * (1 - tempTax)) + ":";
			}
			else
			{
				tvAmountExlTax.Text = ("-");
			}

		}
	}
}