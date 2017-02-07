using System;
namespace BookkeeperLabb2
{
	public class Entry
	{
		
		public int Id { get; private set; }
		public bool Income { get; set; }
		public int Ammount { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public int TypeId { get; set; }
		public int AccountId { get; set; }
		public double TaxRateId { get; set; }

		public Entry()
		{


		}

		public override string ToString()
		{
			return string.Format("[Entry: Id={0}, Income={1}, Ammount={2}, Date={3}, Description={4}, TypeId={5}," +
								 " AccountId={6}, TaxRateId={7}]", Id, Income, Ammount, Date, Description, TypeId,
								 AccountId, TaxRateId);
		}

	}
}
