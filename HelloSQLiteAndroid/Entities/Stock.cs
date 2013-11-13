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
	public class Stock
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		[MaxLength(8)]
		public string Symbol { get; set; }
	}
}

