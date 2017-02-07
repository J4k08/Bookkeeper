using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;

namespace BookkeeperLabb2
{
	public class BookKeeperManager
	{
		public List<Entry> Entries { get; private set; }
		public List<TaxRate> TaxRates { get; private set; }
		public List<Account> MoneyAccount { get; private set; }
		public List<Account> IncomeAccount { get; private set; }
		public List<Account> ExpenseAccount { get; private set; }

		SQLiteConnection db;


		string pathToDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		private static BookKeeperManager instance;

		private BookKeeperManager()
		{
			db = new SQLiteConnection(pathToDb + @"\database.db");

			db.CreateTable<Entry>();
			db.CreateTable<Account>();
			db.CreateTable<TaxRate>();

			Entries = new List<Entry>();

			IncomeAccount = new List<Account> { new Account { Name = "Spelkonsoler", Type = "income", Number = 1000 },
				{ new Account { Name = "Actionfigurer", Type = "income", Number = 1001 } },
				{ new Account { Name = "Spel", Type = "income", Number = 1002 } } };

			ExpenseAccount = new List<Account> { new Account { Name = "Hyra", Type = "expense", Number = 2000 },
				 { new Account { Name = "Personal", Type = "expense", Number = 2001 } },
				 { new Account { Name = "Inköp", Type = "expense", Number = 2003 } } };

			MoneyAccount = new List<Account> { new Account { Name = "Kassa", Type = "MoneyAccount", Number = 3000 },
				 { new Account { Name = "Kassakistan", Type = "MoneyAccount", Number = 3001 } } };

			TaxRates = new List<TaxRate> { new TaxRate
				{ Tax = 0.06 },
				{ new TaxRate { Tax = 0.12 } },
				{ new TaxRate { Tax = 0.25 } } };

		}

		public static BookKeeperManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BookKeeperManager();
				}
				return instance;
			}

		}

		public List<Account> getAccounts(string type)
		{
			return db.Table<Account>().Where(Account => Account.Type.Equals(type)).ToList();
		}

		public Account getOneAccount(int id)
		{
			return db.Get<Account>(id);
		}

		public List<TaxRate> getTaxRates()
		{
			return db.Table<TaxRate>().ToList();
		}
		public TaxRate getTaxRate(int id)
		{
			return db.Get<TaxRate>(id);
		}
		public List<Entry> getEntries() 
		{
			return db.Table<Entry>().ToList();
		}

		public void AddEntry(Entry e)
		{
			db.Insert(e);
			Console.WriteLine(e.ToString());

		}

	}
}
