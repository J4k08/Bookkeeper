using System;
using SQLite;
namespace BookkeeperLabb2
{
	public class Entry : Java.Lang.Object
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; private set; }
		public bool Income { get; set; }
		public int Amount { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public int AccountType { get; set; }
		public int AccountMoney { get; set; }
		public double TaxRate { get; set; }

		public Entry()
		{
		}
		public Entry(bool income, int amount, DateTime date, string description, int accountType, int accountMoney, double taxRate)
		{
			Income = income;
			Amount = amount;
			Date = date;
			Description = description;
			AccountType = accountType;
			AccountMoney = accountMoney;
			TaxRate = taxRate;
		}

		public override string ToString()
		{
			return string.Format("[Entry: Id={0}, Income={1}, Ammount={2}, Date={3}, Description={4}, TypeId={5}," +
								 " AccountId={6}, TaxRateId={7}]", Id, Income, Amount, Date, Description, AccountType,
								 AccountMoney, TaxRate);
		}

	}
}
