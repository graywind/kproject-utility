using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;



class KProjectUtility {
 	public static List<MyGet> packagelist;// = new List<MyGet>();
	//Main Window
	public static Window window;

	//Boxes
	public static VBox box1;
	public static HBox box2;
	public static Box quitbox;
	
	//Labels and separators;
	public static HSeparator separator;

	public static bool firstrun = true;

        public static void Main()
        {


		Application.Init ();
	 
		window = new Window ("Project K");
		window.BorderWidth = 10;


		window.DeleteEvent += new
		DeleteEventHandler (Window_Delete);
	
		Button downloadButton = new Button ("Download");
		downloadButton.Clicked += new EventHandler (downloadButton_Clicked);

		DrawApp( MainTree ( EmptyStore () ) ); 


		window.SetDefaultSize (800, 600);

		window.ShowAll ();

		Application.Run ();
 
        }

	
	public static void Window_Delete (object o, DeleteEventArgs args)
    	{
        	Application.Quit ();
        	args.RetVal = true;
    	}

	public static void downloadButton_Clicked (object o, EventArgs args)
    	{
        	System.Console.WriteLine ("not implemented yet");
    	}

        public static void QuitButton_Clicked(object sender, EventArgs e)
        {
                Application.Quit();
        }



	public static void FetchXml_Clicked(object sender, EventArgs e)
	{
		//string url = "https://www.myget.org/F/aspnetvnext/api/v2/GetUpdates%28%29?packageIds=%27KRE-mono45-x86%27&versions=%270.0%27&includePrerelease=true&includeAllVersions=true";
		string url = "updates.xml";
		packagelist = MyGet.MygetFeed (url);
		DrawApp( MainTree ( TreeStoreGenerator () ) );
		window.ShowAll();

	}
	
	



	//error CS0234: The type or namespace name `TreeModel' does not exist in the namespace `Gtk'. Are you missing an assembly reference?
	/*
	int NormalPackageCompareNodes (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			string name1 = (string)model.GetValue (a, NormalPackageNameID);
			string name2 = (string)model.GetValue (b, NormalPackageNameID);
			return string.Compare (name1, name2, true);
		}
	*/

	public static void tree_CursorChanged(object sender, EventArgs e)
 	{
		 TreeSelection selection = (sender as TreeView).Selection;
		 var model = (sender as TreeView).Model;
		 TreeIter iter;

		 if(selection.GetSelected(out model, out iter)){
 			Console.WriteLine("Selected Value:"+(sender as TreeView).Model.GetValue (iter, 0).ToString()+(sender as TreeView).Model.GetValue (iter, 2).ToString());
 		}
 	}

	public static Gtk.TreeView MainTree ( Gtk.ListStore store )
	{
		Gtk.TreeView localTree = new Gtk.TreeView ();
		
		Gtk.TreeViewColumn packageidColumn  = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn versionColumn  = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn contentColumn = new Gtk.TreeViewColumn ();
		//Gtk.TreeViewColumn copyrightColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn publishedColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn licenseurlColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn licensenamesColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn latestversionColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn packagehashColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn packagehashalgorithmColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn packagesizeColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn summaryColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn versiondownloadcountColumn = new Gtk.TreeViewColumn ();

		packageidColumn.Title = "ID";
		versionColumn.Title = "Version";
		contentColumn.Title = "Link";
		//copyrightColumn.Title  = "Copyright";
		publishedColumn.Title  = "Publish Date";
		licenseurlColumn.Title  = "License Link";
		licensenamesColumn.Title  = "Licenses";
		latestversionColumn.Title  = "Latest Version";
		packagehashColumn.Title  = "Package Hash";
		packagehashalgorithmColumn.Title  = "Hash Algorithm";
		packagesizeColumn.Title  = "Package Size";
		summaryColumn.Title  = "Summary";
		versiondownloadcountColumn.Title = "Download Count";

		//Hide crazy wide columns

		
	
		contentColumn.Visible = false;
		licenseurlColumn.Visible = false;
		latestversionColumn.Visible  = false;
		packagehashColumn.Visible = false;
		packagehashalgorithmColumn.Visible  = false;
		summaryColumn.Visible = false;


		Gtk.CellRendererText packageidCell  = new Gtk.CellRendererText();
		Gtk.CellRendererText versionCell  = new Gtk.CellRendererText ();
		Gtk.CellRendererText contentCell = new Gtk.CellRendererText ();
		//Gtk.CellRendererText copyrightCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText publishedCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText licenseurlCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText licensenamesCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText latestversionCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText packagehashCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText packagehashalgorithmCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText packagesizeCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText summaryCell = new Gtk.CellRendererText ();
		Gtk.CellRendererText versiondownloadcountCell = new Gtk.CellRendererText ();

		packageidColumn.PackStart ( packageidCell, true);
		versionColumn.PackStart ( versionCell, true);
		contentColumn.PackStart ( contentCell, true);
		//copyrightColumn.PackStart ( copyrightCell, true);
		publishedColumn.PackStart ( publishedCell, true);
		licenseurlColumn.PackStart ( licenseurlCell, true);
		licensenamesColumn.PackStart ( licensenamesCell, true);
		latestversionColumn.PackStart ( latestversionCell, true);
		packagehashColumn.PackStart ( packagehashCell, true);
		packagehashalgorithmColumn.PackStart ( packagehashalgorithmCell, true);
		packagesizeColumn.PackStart ( packagesizeCell, true);
		summaryColumn.PackStart ( summaryCell, true);
		versiondownloadcountColumn.PackStart ( versiondownloadcountCell, true);
		
		localTree.AppendColumn (packageidColumn);
		localTree.AppendColumn (versionColumn);
		localTree.AppendColumn (contentColumn);
		//localTree.AppendColumn (copyrightColumn);
		localTree.AppendColumn (publishedColumn);
		localTree.AppendColumn (licenseurlColumn);
		localTree.AppendColumn (licensenamesColumn);
		localTree.AppendColumn (latestversionColumn);
		localTree.AppendColumn (packagehashColumn);
		localTree.AppendColumn (packagehashalgorithmColumn);
		localTree.AppendColumn (packagesizeColumn);
		localTree.AppendColumn (summaryColumn);
		localTree.AppendColumn (versiondownloadcountColumn);

		packageidColumn.AddAttribute ( packageidCell, "text", 0);
		versionColumn.AddAttribute ( versionCell, "text", 1);
		contentColumn.AddAttribute ( contentCell, "text", 2);
		//copyrightColumn.AddAttribute ( copyrightCell, "text", 0);
		publishedColumn.AddAttribute ( publishedCell, "text", 3);
		licenseurlColumn.AddAttribute ( licenseurlCell, "text", 4);
		licensenamesColumn.AddAttribute ( licensenamesCell, "text", 5);
		latestversionColumn.AddAttribute ( latestversionCell, "text", 6);
		packagehashColumn.AddAttribute ( packagehashCell, "text", 7);
		packagehashalgorithmColumn.AddAttribute ( packagehashalgorithmCell, "text", 8);
		packagesizeColumn.AddAttribute ( packagesizeCell, "text", 9);
		summaryColumn.AddAttribute ( summaryCell, "text", 10);
		versiondownloadcountColumn.AddAttribute ( versiondownloadcountCell, "text", 11);

		//store.DefaultSortFunc = CustomSort;
		store.SetSortColumnId(1,Gtk.SortType.Descending);
 
		localTree.Model = store;
		localTree.CursorChanged += new EventHandler(tree_CursorChanged);






		return localTree;
	}

	public static Gtk.ListStore EmptyStore ()
	{
		Gtk.ListStore packageListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string));
		return packageListStore;	
	}


	public static Gtk.ListStore TreeStoreGenerator ()
	{
		Gtk.ListStore packageListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string));

		packagelist.ForEach(wow => packageListStore.AppendValues(wow.packageid, wow.version, wow.content, wow.published.ToString(), wow.licenseurl, wow.licensenames, wow.latestversion.ToString(), wow.packagehash, wow.packagehashalgorithm, wow.packagesize, wow.summary, wow.versiondownloadcount.ToString())); 


		
 		
		return packageListStore;	
	}
	

	public static void InitialDraw()
	{
		
	}
	public static void DrawApp ( Gtk.TreeView localTree )
	{
		if (firstrun == false)
		{
			window.Remove(box1);
		}
		else
		{
			firstrun = false;
		}
		
		ScrolledWindow sw = new ScrolledWindow();
	
		separator = new HSeparator ();
		box1 = new VBox (false, 0);
		sw.Add(localTree);
		box1.PackStart (sw, true, true, 5);
		box1.PackStart (separator, false, true, 5);
               	separator.Show();

		box2 = new HBox (false, 0);

		quitbox = new HBox (false, 0);

		Button downloadButton = new Button ("Download");
		downloadButton.Clicked += new EventHandler (downloadButton_Clicked);

                Button button = new Button ("Quit");
                button.Clicked += QuitButton_Clicked;

		Button FetchXml = new Button ("Fetch Feed");
		FetchXml.Clicked += FetchXml_Clicked;		

		quitbox.PackStart(FetchXml, true, false, 0);
		quitbox.PackStart(downloadButton, true, false, 0);
                quitbox.PackStart(button, true, false, 0);


 
		box1.PackStart (box2, false, false, 0);                                        
		box1.PackStart (quitbox, false, false, 0);
		
 
                window.Add(box1);
	}
}
