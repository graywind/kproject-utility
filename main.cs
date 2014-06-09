using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Diagnostics; //provides Stopwatch
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO.Compression.ZipArchive;



class KProjectUtility {
 	public static List<MyGet> packagelist;
	//Main Window
	public static Window window;
	public static Window downloadWindow;

	//Download
	public static WebClient webClient;
	public static Stopwatch sw;
	public static Label labelSpeed;
	public static Label labelPerc;
	public static Label labelDownloaded;
	public static Label downloadLabel;
	public static ProgressBar progressBar;
	

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

		window.SetPosition(Gtk.WindowPosition.Center);

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
        	//System.Console.WriteLine ("not implemented yet");
		DownloadWindow();
		//InfoModal("Download completed!", window);
    	}

        public static void QuitButton_Clicked(object sender, EventArgs e)
        {
                Application.Quit();
        }



	public static void FetchXml_Clicked(object sender, EventArgs e)
	{
		/*
		string url = "https://www.myget.org/F/aspnetvnext/api/v2/GetUpdates()" +
			"?packageIds='KRE-mono45-x86'&versions='0.0'" + 
			"&includePrerelease=true&includeAllVersions=true";
		*/
		string url = "updates.xml";
		packagelist = MyGet.MygetFeed (url);
		DrawApp( MainTree ( TreeStoreGenerator () ) );
		window.ShowAll();

	}
	
	public static void tree_CursorChanged(object sender, EventArgs e)
 	{
		 TreeSelection selection = (sender as TreeView).Selection;
		 var model = (sender as TreeView).Model;
		 TreeIter iter;

		 if(selection.GetSelected(out model, out iter)){
 			Console.WriteLine("Selected Value:"+(sender as TreeView).Model.GetValue (iter, 0).ToString() +
					(sender as TreeView).Model.GetValue (iter, 2).ToString());
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
		Gtk.ListStore packageListStore	= new Gtk.ListStore 
			(typeof (string),
			typeof (string),
			typeof (string), 
			typeof (string),
			typeof (string), 
			typeof (string), 
			typeof (string),
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string));
		return packageListStore;	
	}


	public static Gtk.ListStore TreeStoreGenerator ()
	{
		Gtk.ListStore packageListStore = new Gtk.ListStore 
			(typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string), 
			typeof (string));

		packagelist.ForEach(
			wow => packageListStore.AppendValues(
				wow.packageid, 
				wow.version, 
				wow.content, 
				wow.published.ToString(), 
				wow.licenseurl, 
				wow.licensenames, 
				wow.latestversion.ToString(), 
				wow.packagehash, 
				wow.packagehashalgorithm, 
				wow.packagesize, 
				wow.summary, 
				wow.versiondownloadcount.ToString())
			); 
		return packageListStore;	
	}
	
	public static void ExceptionModal (string exceptionText, Window parentWindow)
	{
	    var dialog = new Gtk.MessageDialog (
		parentWindow, 
		Gtk.DialogFlags.Modal,
		Gtk.MessageType.Error, 
		Gtk.ButtonsType.Close, 
		exceptionText);
	    dialog.Run ();
	    dialog.Destroy ();
	}

	public static void InfoModal (string infoText, Window parentWindow)
	{
	    var dialog = new Gtk.MessageDialog (
		parentWindow, 
		Gtk.DialogFlags.Modal,
		Gtk.MessageType.Info, 
		Gtk.ButtonsType.Close, 
		infoText);
	    dialog.Run ();
	    dialog.Destroy ();
	}

	public static void DownloadWindow ()
	{

		sw = new Stopwatch();
		labelSpeed = new Label();
		labelPerc = new Label();
		labelDownloaded = new Label();
		progressBar = new ProgressBar();
		downloadWindow = new Window("Download Window");

		downloadWindow.BorderWidth = 10;
		downloadWindow.TransientFor = window;
		downloadWindow.SetPosition(Gtk.WindowPosition.CenterOnParent);	
		downloadWindow.Modal = true;	
		downloadWindow.SetDefaultSize (500, 100);
		
		VBox downloadVBox = new VBox (false, 0);
	
		downloadLabel = new Label("");
		progressBar.Text = "Download starting...";
		progressBar.ShowText = true;
		progressBar.Fraction = 0.0;

		Button cancelDownloadButton = new Button ("Cancel");
		cancelDownloadButton.Clicked += new EventHandler (CancelDownload);

		downloadVBox.PackStart (progressBar, true, true, 5);
		downloadVBox.PackStart (cancelDownloadButton, true, true, 5);
		downloadVBox.PackStart (downloadLabel, true, true, 5);
		
	
		downloadWindow.Add(downloadVBox);

		DownloadFile("http://mirrors.kernel.org/archlinux/iso/2014.05.01/archlinux-2014.05.01-dual.iso", Environment.GetEnvironmentVariable("HOME") + "/test.iso");
		
		downloadWindow.ShowAll();
		//dl.progressBar.Window.ProcessUpdates(true);
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

	public static void DownloadFile(string urlAddress, string location)
	{
	    using (webClient = new WebClient())
	    {
		webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
		webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

		Uri URL = new Uri(urlAddress);
	 
		sw.Start();
	 
		try
		{

		    webClient.DownloadFileAsync(URL, location);
		}
		catch (Exception ex)
		{
		    ExceptionModal(ex.Message, downloadWindow);
		}
	    }
	}
	
	private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
	    // Calculate download speed and output it to labelSpeed.
	    labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
	 
	    // Update the progressbar percentage only when the value is not the same.
	    progressBar.Fraction = e.ProgressPercentage/100.0;
	 
	    // Show the percentage on our label.
	    labelPerc.Text = e.ProgressPercentage.ToString() + "%";
	 
	    // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
	    labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
		(e.BytesReceived / 1024d / 1024d).ToString("0.00"),
		(e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));

	    // Gtk.ProgressBar supports superimposed text so label is not needed.
	    progressBar.Text = string.Format("{0} MB's / {1} MB's",
		(e.BytesReceived / 1024d / 1024d).ToString("0.00"),
		(e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
	   
	    
	}

	public static void CancelDownload(object sender, EventArgs e)
	{	
		if (webClient != null)
		{
			webClient.CancelAsync();
			webClient = null;
		}
	}	

	public static void Completed(object sender, AsyncCompletedEventArgs e)
	{
	    sw.Reset();
	 
	    if (e.Cancelled == true)
	    {
		//InfoModal("Download has been canceled.", downloadWindow);
		progressBar.Text = "Cancelled!";
		progressBar.Fraction = 0.0;
	    }
	    else
	    {
		//InfoModal("Download completed!", downloadWindow);
	    }
	}

	
	

}
