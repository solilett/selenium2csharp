using System;
using System.Xml;
using System.Text;
using System.IO;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	/// <summary>
	/// Reads the XML of a selenium test case.
	/// </summary>
	public class TestCaseReader
	{		
		private HtmlDocument testCaseDocument;
		public HtmlDocument TestCaseDocument
		{
			get { return testCaseDocument; }
			set { testCaseDocument = value; }
		}
		
		public TestCaseReader (string testCaseHtml)
		{
			TestCaseDocument = new HtmlDocument();
			TestCaseDocument.LoadHtml(testCaseHtml);
		}

		/// <summary>
		/// Reads the command nodes from the selenium test case. 
		/// </summary>
		/// <returns>
		/// A <see cref="HtmlNodeCollection"/>
		/// </returns>
		public HtmlNodeCollection ReadCommands ()
		{
			HtmlNodeCollection rows = TestCaseDocument.DocumentNode.SelectNodes("//html/body/table/tbody/tr");
			
			return rows;
		}
		
		/// <summary>
		/// Reads the base URL of the selenium test case. 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string ReadBaseUrl()
		{
			HtmlNode baseLinkNode = TestCaseDocument.DocumentNode.SelectSingleNode("//html/head/link[@rel='selenium.base']");
			
			string baseUrl = baseLinkNode.Attributes["href"].Value;
			
			return baseUrl;
		}
		
		/// <summary>
		/// Reads the name the selenium test case. 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string ReadName()
		{
			HtmlNode titleNode = TestCaseDocument.DocumentNode.SelectSingleNode("//html/head/title");
			
			string name = titleNode.InnerText;
			
			return name;
		}
	}
}
