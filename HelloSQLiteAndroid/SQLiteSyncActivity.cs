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
	[Activity (Label = "SQLiteSyncActivity", MainLauncher = true)]			
	public class SQLiteSyncActivity : Activity
	{
		Button btnCreate;
		EditText txtCreate;
		SQLiteConnection sqliteCnn;
		string folder;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SQLiteSync);

			InitComponent ();
			// Create your application here

			btnCreate.Click += (sender, e) => {;

				folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				if (System.IO.File.Exists(System.IO.Path.Combine(folder, txtCreate.Text))) {
					CreateAlertDialog("That Database name already exists",1);
				}
				else {
					sqliteCnn = new SQLiteConnection(System.IO.Path.Combine(folder, txtCreate.Text));
					sqliteCnn.CreateTable<Stock>();
					sqliteCnn.CreateTable<Valuation>();
					CreateAlertDialog("Database successfully created",2);
				}
			};
		}
		private void InitComponent(){
			btnCreate = FindViewById<Button> (Resource.Id.btnCreate);
			txtCreate = FindViewById<EditText> (Resource.Id.txtCreateDB);
		}

		private void CreateAlertDialog(string dialog, int flag) {
			Android.App.AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog alertDialog = builder.Create ();
			alertDialog.SetTitle ("Aviso");
			alertDialog.SetMessage (dialog);

			alertDialog.SetButton ("OK", (sender, e) => {
				switch (flag) {
				case 1:
					alertDialog.Hide();
					break;
				case 2:
					//StartActivity(typeof(SQLiteSyncSearchActivity));
					var next = new Intent(this, typeof(SQLiteSyncSearchActivity));
					next.PutExtra("DataBasePath",System.IO.Path.Combine(folder, txtCreate.Text));
					StartActivity(next);
					break;
				default:
					break;
				}
			}); 
			alertDialog.Show ();
		}

	}
}

