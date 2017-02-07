using System;
namespace BookkeeperLabb2
{
	public class TaxRate
	{
		
		public int Id { get; set; }
		public double Tax { get; set; }

		public TaxRate()
		{

		}

		public override string ToString()
		{
			return string.Format(Tax * 100 + "%");
		}
	}
}
