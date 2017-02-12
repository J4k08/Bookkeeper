using System;
using SQLite;
namespace BookkeeperLabb2
{
	public class Account : Java.Lang.Object
	{
		[PrimaryKey]
		public int Number { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }

		public Account()
		{

		}
		/* Choose to override ToString to be able to debug easier */
		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, Number);
		}

	}
}
