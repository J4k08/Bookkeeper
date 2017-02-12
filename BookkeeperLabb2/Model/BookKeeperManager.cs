using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;

namespace BookkeeperLabb2
{
	public class BookKeeperManager
	{
		//public List<Entry> Entries { get; private set; }
		//public List<TaxRate> TaxRates { get; private set; }
		//public List<Account> MoneyAccount { get; private set; }
		//public List<Account> IncomeAccount { get; private set; }
		//public List<Account> ExpenseAccount { get; private set; }

		SQLiteConnection db;
		string pathToDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

		private static BookKeeperManager instance;

		private BookKeeperManager()
		{
			db = new SQLiteConnection(pathToDb + @"\database.db");

			db.CreateTable<Entry>();
			db.CreateTable<Account>();
			db.CreateTable<TaxRate>();

			if (db.Table<Account>().Count() == 0)
			{

				db.Insert(new Account { Name = "Spelkonsoler", Type = "income", Number = 1000 });
				db.Insert(new Account { Name = "Actiofigurer", Type = "income", Number = 1001 });
				db.Insert(new Account { Name = "Spel", Type = "income", Number = 1002 });

				db.Insert(new Account { Name = "Hyra", Type = "expense", Number = 2000 });
				db.Insert(new Account { Name = "Personal", Type = "expense", Number = 2001 });
				db.Insert(new Account { Name = "Inköp", Type = "expense", Number = 2002 });

				db.Insert(new Account { Name = "Kassan", Type = "moneyAccount", Number = 3000 });
				db.Insert(new Account { Name = "Skattkistan", Type = "moneyAccount", Number = 3001 });

			}
			if (db.Table<TaxRate>().Count() == 0)
			{
				db.Insert(new TaxRate { Tax = 0.06 });
				db.Insert(new TaxRate { Tax = 0.12 });
				db.Insert(new TaxRate { Tax = 0.20 });
			}
		}

		/* Singleton constructor. If the BookkeeperManager object doesn't already exists, it'll create one, otherwise
		 * it'll return (BookKeeperManager instance) */
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
		/* returns a List of Accounts from the sqlite-database */
		public List<Account> GetAccounts(string type)
		{
			return db.Table<Account>().Where(Account => Account.Type.Equals(type)).ToList();
		}
		/* returns a List of TaxRates from the sqlite-database */
		public List<TaxRate> GetTaxRates()
		{
			return db.Table<TaxRate>().ToList();
		}
		/* returns a List of Entries from the sqlite-database */
		public List<Entry> GetEntries()
		{
			return db.Table<Entry>().ToList();
		}
		/* Method for adding an Entry into the sqlite-database */ 
		public void AddEntry(Entry e)
		{
			db.Insert(e);
			Console.WriteLine(e.ToString());
		}

		/* (Var taxReport) is returned after adding all the Entries from the sqlite-database. If e.isIncome is true, 
		it returns a positive number, else if e.isincome is false, it'll return a negative number. */
		public string GetTaxReport()
		{
			var taxReport = GetEntries().Select(e => string.Format("{0}, {1}. Moms: {2}:- ",e.Date.ToString("yyyy-MM-dd"),
			                                                       e.Description,
			                                                       (e.isIncome ? (e.Amount * e.TaxRate):(e.Amount * e.TaxRate)*-1)));

			return string.Join("\n", taxReport);
		}
	}
}
