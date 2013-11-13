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
	[Activity (Label = "SQLiteSyncSearchActivity")]			
	public class SQLiteSyncSearchActivity : Activity
	{
		EditText txtStock;
		Button btnSearch, btnAddStock;
		ListView lstStock;
		string dbPath;
		List<Stock> myStocks;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			dbPath = Intent.GetStringExtra ("DataBasePath"); 
			SQLiteConnection sqLiteConn = new SQLiteConnection (dbPath);
			
			// Create your application here
			SetContentView (Resource.Layout.SQLiteSyncStocks);
			InitComponent ();

			myStocks = sqLiteConn.Table<Stock> ().ToList ();
			lstStock.Adapter = new StocksAdapters (this, myStocks);


			btnSearch.Click += (object sender, EventArgs e) => {
				if (txtStock.Text != "")
				{
					myStocks = sqLiteConn.Table<Stock>().Where(x => x.Symbol.Contains(txtStock.Text)).ToList();
					lstStock.Adapter = new StocksAdapters(this, myStocks);
				}
				else {
					lstStock.Adapter = new StocksAdapters(this, sqLiteConn.Table<Stock>().ToList());
				}
			};

			btnAddStock.Click += (object sender, EventArgs e) => {
				//StartActivity(typeof(SQLiteSyncNewActivity));
				var next = new Intent(this, typeof(SQLiteSyncNewActivity));
				next.PutExtra("DataBasePath", dbPath);
				StartActivity(next);
			};
		}

		private void InitComponent()
		{
			txtStock = FindViewById<EditText> (Resource.Id.txtStock);
			btnSearch = FindViewById<Button> (Resource.Id.btnSearch);
			lstStock = FindViewById<ListView> (Resource.Id.lstStock);
			btnAddStock = FindViewById<Button> (Resource.Id.btnAddStock);
		}
	}
}

