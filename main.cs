using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
 

public class MyGet {
	public string packageid { get; set;}
	public string version { get; set;}
	public string content { get; set; }
	public string copyright { get; set; }
	public DateTime published { get; set; }
	public string licenseurl { get; set;}
	public string licensenames { get; set; }
	public bool latestversion { get; set; }
	public string packagehash { get; set; }
	public string packagehashalgorithm { get; set; }
	public Int64 packagesize { get; set;}
	public string summary { get; set; }
	public Int32 versiondownloadcount { get; set; }

	public MyGet(string _packageid, string _version, string _content, string _published, string _licenseurl, string _licensenames, string _latestversion, string _packagehash, string _packagehashalgorithm, string _packagesize, string _summary, string _versiondownloadcount)
	{
		packageid = _packageid;
		version = _version;
		content = _content;
		published = DateTime.Parse(_published);
		licenseurl = _licenseurl;
		licensenames = _licensenames;
		latestversion = bool.Parse(_latestversion);
		packagehash = _packagehash;
		packagehashalgorithm = _packagehashalgorithm;
		packagesize = Int64.Parse(_packagesize);
		summary = _summary;
		versiondownloadcount = Int32.Parse(_versiondownloadcount);
	}

}

 public static class Extensions
    {
        public static void WriteToConsole<T>(this IList<T> collection)
        {
            WriteToConsole<T>(collection, "\t");
        }

        public static void WriteToConsole<T>(this IList<T> collection, string delimiter)
        {
             collection.FastForEach(item => Console.Write("{0}{1}", item.ToString(), delimiter));
        }

        public static void FastForEach<T>(this IList<T> collection, Action<T> actionToPerform)
        {
            int count = collection.Count();
            for (int i = 0; i < count; ++i)
            {
                actionToPerform(collection[i]);    
            }
            Console.WriteLine();
        }
    }

class Hello {
 	public static List<MyGet> myget;// = new List<MyGet>();
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
		XElement FetchedFeed = XElement.Load("https://www.myget.org/F/aspnetvnext/api/v2/GetUpdates%28%29?packageIds=%27KRE-mono45-x86%27&versions=%270.0%27&includePrerelease=true&includeAllVersions=true");
		//XElement FetchedFeed = XElement.Load("updates.xml");
		MygetFeedToTreeStore ( FetchedFeed );

	}
	
	// Will eventually return a Gtk.TreeStore from a passed XML
	public static void MygetFeedToTreeStore (XElement Feed)
	{
		myget = new List<MyGet>();
		string nsSchema = "http://www.w3.org/2005/Atom";
		string dSchema = "http://schemas.microsoft.com/ado/2007/08/dataservices";
		string mSchema = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

		//XNamespace m = mSchema;
		//XNamespace d = dSchema;
		//XNamespace ns = nsSchema;
		XName entry = XName.Get("entry",nsSchema);
			//XName id = XName.Get("id",nsSchema); //<entry><id>
			XName content = XName.Get("content",nsSchema); //<entry><content>
				XName properties = XName.Get("properties",mSchema); //<entry><m:properties>
					XName dId 						= 		XName.Get("Id", dSchema); //<entry><m:properties><d:Id>
					XName dVersion 					= 		XName.Get("Version", dSchema); //<entry><m:properties><d:Version>
					//XName dAuthors 					= 		XName.Get("Authors", dSchema); //<entry><m:properties><d:Authors>
					//XName dCopyright 				= 		XName.Get("Copyright", dSchema); //<entry><m:properties><d:Copyright>
					XName dPublished 				= 		XName.Get("Published", dSchema); //<entry><m:properties><d:Published m:type="Edm.DateTime">
					XName dLicenseUrl 				= 		XName.Get("LicenseUrl", dSchema); //<entry><m:properties><d:LicenseUrl>
					XName dLicenseNames				= 		XName.Get("LicenseNames", dSchema); //<entry><m:properties><d:LicenseName>
					XName dIsAbsoluteLatestVersion 	= 		XName.Get("IsAbsoluteLatestVersion", dSchema); //<entry><m:properties><d:IsAbsoluteLatestVersion m:type="Edm.Boolean">
					XName dPackageHash 				= 		XName.Get("PackageHash", dSchema); //<entry><m:properties><d:PackageHash>
					XName dPackageHashAlgorithm		= 		XName.Get("PackageHashAlgorithm", dSchema); //<entry><m:properties><d:PackageHashAlgorithm>
					XName dPackageSize				=		XName.Get("PackageSize", dSchema); //<entry><m:propterties><d:PackageSize m:type="Edm.Int64">
					XName dSummary	 				= 		XName.Get("Summary", dSchema); //<entry><m:properties><d:Summary>
					XName dVersionDownloadCount		= 		XName.Get("VersionDownloadCount", dSchema); //<entry><m:properties><d:VersionDownloadCount m:type="Edm.Int32"
	


					
		foreach (var entryElement in Feed.Elements(entry))
		{

			
			//var idElement = entryElement.Element(id);
			var contentElement = entryElement.Element(content);

				var propElement = entryElement.Element(properties);
					var dIdElement = propElement.Element(dId);
					var dVersionElement = propElement.Element(dVersion);
					//var dAuthorsElement = propElement.Element(dAuthors);
					//var dCopyrightElement = propElement.Element(dCopyright);
					var dPublishedElement = propElement.Element(dPublished);
					var dLicenseUrlElement = propElement.Element(dLicenseUrl);
					var dLicenseNamesElement = propElement.Element(dLicenseNames);
					var dIsAbsoluteLatestVersionElement = propElement.Element(dIsAbsoluteLatestVersion);
					var dPackageHashElement = propElement.Element(dPackageHash);
					var dPackageHashAlgorithmElement = propElement.Element(dPackageHashAlgorithm);
					var dPackageSizeElement = propElement.Element(dPackageSize);
					var dSummaryElement = propElement.Element(dSummary);
					var dVersionDownloadCountElement = propElement.Element(dVersionDownloadCount);


					myget.Add(new MyGet(
						dIdElement.Value, 
						dVersionElement.Value, 
						contentElement.Attribute("src").Value, 
						dPublishedElement.Value,
						dLicenseUrlElement.Value,
						dLicenseNamesElement.Value,
						dIsAbsoluteLatestVersionElement.Value,
						dPackageHashElement.Value,				
						dPackageHashAlgorithmElement.Value,
						dPackageSizeElement.Value,
						dSummaryElement.Value,
						dVersionDownloadCountElement.Value
						));
					

				
		}

		//myget.ForEach(wow => Console.Write("{0}\t{1}\t{2}\n", wow.packageid, wow.version, wow.published.ToString()));
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
		Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string));
		return musicListStore;	
	}


	public static Gtk.ListStore TreeStoreGenerator ()
	{
		Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string), typeof (string));

		myget.ForEach(wow => musicListStore.AppendValues(wow.packageid, wow.version, wow.content, wow.published.ToString(), wow.licenseurl, wow.licensenames, wow.latestversion.ToString(), wow.packagehash, wow.packagehashalgorithm, wow.packagesize, wow.summary, wow.versiondownloadcount.ToString())); 


		
 		
		return musicListStore;	
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
