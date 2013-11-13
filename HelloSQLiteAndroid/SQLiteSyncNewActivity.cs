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
	[Activity (Label = "SQLiteSyncNewActivity")]			
	public class SQLiteSyncNewActivity : Activity
	{		
		EditText txtNew;
		Button btnNew;
		string dbPath;
		Stock objStock;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.SQLiteSyncNewStock);
			InitComponent ();

			btnNew.Click += (object sender, EventArgs e) => {
				objStock = new Stock() {
					Symbol = txtNew.Text
				};
				using(SQLiteConnection sqLiteCnn = new SQLiteConnection(dbPath))
				 {
					sqLiteCnn.Insert(objStock);
					//Console.WriteLine("{0} == {1}", objStock.Symbol, objStock.Id);
					CreateAlertDialog("The Stock " + objStock.Symbol + " has been created with Id: " + objStock.Id);
				 }

			};
		}

		private void InitComponent()
		{
			dbPath = Intent.GetStringExtra ("DataBasePath");
			txtNew = FindViewById<EditText> (Resource.Id.txtNew);
			btnNew = FindViewById<Button> (Resource.Id.btnNew);
		}

		private void CreateAlertDialog(string dialog) {
			Android.App.AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog alertDialog = builder.Create ();
			alertDialog.SetTitle ("Aviso");
			alertDialog.SetMessage (dialog);

			alertDialog.SetButton ("OK", (sender, e) => {

					var next = new Intent(this, typeof(SQLiteSyncSearchActivity));
					next.PutExtra("DataBasePath",dbPath);
					StartActivity(next);
					

			}); 
			alertDialog.Show ();
		}
	}
}

