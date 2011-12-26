using System;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	public class TestSuiteReader
	{
		private HtmlDocument testSuiteDocument;
		public HtmlDocument TestSuiteDocument
		{
			get { return testSuiteDocument; }
			set { testSuiteDocument = value; }
		}
		
		public TestSuiteReader (string testSuiteHtml)
		{
			TestSuiteDocument = new HtmlDocument();
			TestSuiteDocument.LoadHtml(testSuiteHtml);
		}
		
		public TestSuiteReader ()
		{
		}
		
		/// <summary>
		/// Reads the test case nodes from the test suite.
		/// </summary>
		/// <returns>
		/// A <see cref="HtmlNodeCollection"/>
		/// </returns>
		public HtmlNodeCollection ReadCases ()
		{
			HtmlNodeCollection links = TestSuiteDocument.DocumentNode.SelectNodes("//html/body/table/tbody/tr/td/a");
			
			return links;
		}
	}
}

