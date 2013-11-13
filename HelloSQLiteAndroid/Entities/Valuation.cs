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
using SQLite;

namespace HelloSQLiteAndroid
{
	public class Valuation
	{
			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
			[Indexed]
			public int StockId { get; set; }
			public DateTime Time { get; set; }
			public decimal Price { get; set; }

	}
}

