
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
		private string amountChanged;
		private int amount;
		private int type;
		private int moneyAccount;
		private double tax;
		private double tempTax;
		private string description = "";
		private bool isIncome = true;

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

			SetAdapters(isIncome);




			/* Method for changing (TextView tvAmountExlTax) whenever you type or change digits in (EditText etAmount)
			method call CalculateTaxFree() and uses the string created from the EditText as an argument!*/
			etAmount.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
			{
				amountChanged = e.Text.ToString();
				CalculateTaxFree(amountChanged);
			};

			/* Whenever (Button btnAddEntry) is clicked, it calls on SetEntryValues() and then creates an Entry object. Then it
			calls BookkeeperManager's AddEntry() with the Entry as an argument. */
			btnAddEntry.Click += delegate
			{
				SetEntryValues();
				Entry e = new Entry
				{
					isIncome = isIncome,
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
			/* Whenever (RadioButton rbIncome) is Checked, it changes (Bool income) to true and calls method SetTypeSpinner() */
			rbIncome.Click += delegate
			{
				if (rbIncome.Checked)
				{
					Console.WriteLine(isIncome = true);
					SetTypeSpinner(isIncome);
				}
			};
			/* Whenever (RadioButton rbExpense) is checked, it changes (Bool income) to false and calls method SetTypeSpinner() */ 
			rbExpense.Click += delegate
			{
				if (rbExpense.Checked)
				{
					Console.WriteLine(isIncome = false);
					SetTypeSpinner(isIncome);
				}

			};

			/* If you click on one of the elements in (Spinner spTax), it'll set (Double tempTax) to it's correct value and call on
			 * CalculateTaxfree() */
			spTax.ItemSelected += delegate {
				
				tempTax = ((TaxRate)spTax.SelectedItem).Tax;
				CalculateTaxFree(amountChanged);
				
			};

		}

		/* Method for DatePicker, it opens a fragment and whatever date you click on is saved in (DateTime time) */
		void DateSelect(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
				btnDate.Text = time.ToString("yy-MM-dd");
				dateTime = time;
																	 });
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}

		/* Method for setting ArrayAdapters, it calls SetMoneyAccountSpinner(), SetTypeSpinner() and SetTaxRateSpinner() */
		private void SetAdapters(bool b)
		{
			SetMoneyAccountSpinner();
			SetTypeSpinner(isIncome);
			SetTaxRateSpinner();

		}
		/* Method for getting the MoneyAccounts from BookKeeperManager into (Spinner spMoneyAccount) using an ArrayAdapter */
		private void SetMoneyAccountSpinner()
		{
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
														BookKeeperManager.Instance.GetAccounts("moneyAccount"));
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spMoneyAccount.Adapter = typeAdapter;
		}
		/* Based on (Bool income), either income or expense accounts will be set in (Spinner spAccount) */
		private void SetTypeSpinner(bool income)
		{
			if (income)
			{
				ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
															BookKeeperManager.Instance.GetAccounts("income"));
				typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				spAccount.Adapter = typeAdapter;
			}
			else
			{
				ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
															BookKeeperManager.Instance.GetAccounts("expense"));
				typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				spAccount.Adapter = typeAdapter;
			}
		}
		/* Method for getting the TaxRates from BookKeeperManager into (Spinner spTax) */ 
		private void SetTaxRateSpinner()
		{
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
														BookKeeperManager.Instance.GetTaxRates());
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spTax.Adapter = typeAdapter;
		}
		/* Method for setting different variables, somewhat redundant method but I didn't want too much code 
		 * in my btnAddEntry-method */
		private void SetEntryValues()
		{
			description = etDescription.Text;
			amount = Int32.Parse(etAmount.Text.ToString());

			moneyAccount = ((Account)spMoneyAccount.SelectedItem).Number;


			tax = ((TaxRate)spTax.SelectedItem).Tax;

			if (isIncome)
			{
				ac = BookKeeperManager.Instance.GetAccounts("income")[spAccount.SelectedItemPosition];
			}
			else
			{
				ac = BookKeeperManager.Instance.GetAccounts("expense")[spAccount.SelectedItemPosition];
			}

			type = ((Account)spAccount.SelectedItem).Number;

		}
		/* Method for changing the values in the (TextView tvAmountExcTax). If Int32.TryParse returns true it means there
		 * were numbers in (EditText etAmount) and it successfully converted them. If it returns false it'll just set 
		 * (TextView tvAmountExlTax) to "-" */
		private void CalculateTaxFree(string s)
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