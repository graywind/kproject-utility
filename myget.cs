using System;
using System.Collections.Generic;
using System.Xml.Linq;

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

	public MyGet (	string _packageid, 
			string _version, 
			string _content, 
			string _published, 
			string _licenseurl, 
			string _licensenames, 
			string _latestversion, 
			string _packagehash, 
			string _packagehashalgorithm, 
			string _packagesize, 
			string _summary, 
			string _versiondownloadcount )
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

	public static List<MyGet> MygetFeed (string url)
	{
		XElement Feed = XElement.Load(url);
		List<MyGet> myget = new List<MyGet>();
		string nsSchema = "http://www.w3.org/2005/Atom";
		string dSchema = "http://schemas.microsoft.com/ado/2007/08/dataservices";
		string mSchema = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

		//XNamespace m = mSchema;
		//XNamespace d = dSchema;
		//XNamespace ns = nsSchema;
		XName entry = XName.Get("entry",nsSchema);
		//XName id = XName.Get("id",nsSchema); 
			//<entry><id>
		XName content = XName.Get("content",nsSchema); 
			//<entry><content>
		XName properties = XName.Get("properties",mSchema); 
			//<entry><m:properties>
		XName dId = XName.Get("Id", dSchema); 
			//<entry><m:properties><d:Id>
		XName dVersion = XName.Get("Version", dSchema); 
			//<entry><m:properties><d:Version>
		//XName dAuthors = XName.Get("Authors", dSchema); 
			//<entry><m:properties><d:Authors>
		//XName dCopyright = XName.Get("Copyright", dSchema); 
			//<entry><m:properties><d:Copyright>
		XName dPublished = XName.Get("Published", dSchema); 
			//<entry><m:properties><d:Published m:type="Edm.DateTime">
		XName dLicenseUrl = XName.Get("LicenseUrl", dSchema); 
			//<entry><m:properties><d:LicenseUrl>
		XName dLicenseNames = XName.Get("LicenseNames", dSchema); 
			//<entry><m:properties><d:LicenseName>
		XName dIsAbsoluteLatestVersion = XName.Get("IsAbsoluteLatestVersion", dSchema); 
			//<entry><m:properties><d:IsAbsoluteLatestVersion m:type="Edm.Boolean">
		XName dPackageHash = XName.Get("PackageHash", dSchema); 
			//<entry><m:properties><d:PackageHash>
		XName dPackageHashAlgorithm = XName.Get("PackageHashAlgorithm", dSchema); 
			//<entry><m:properties><d:PackageHashAlgorithm>
		XName dPackageSize = XName.Get("PackageSize", dSchema); 
			//<entry><m:propterties><d:PackageSize m:type="Edm.Int64">
		XName dSummary = XName.Get("Summary", dSchema); 
			//<entry><m:properties><d:Summary>
		XName dVersionDownloadCount = XName.Get("VersionDownloadCount", dSchema); 
			//<entry><m:properties><d:VersionDownloadCount m:type="Edm.Int32"
					
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
		return myget;
	}
}


