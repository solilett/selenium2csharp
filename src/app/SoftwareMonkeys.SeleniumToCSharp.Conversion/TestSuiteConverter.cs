using System;
using System.IO;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	public class TestSuiteConverter
	{
		private string testSuiteHtml;
		/// <summary>
		/// Gets/sets the test case HTML.
		/// </summary>
		public string TestSuiteHtml
		{
			get { return testSuiteHtml; }
			set { testSuiteHtml = value; }
		}
		
		private string testNamespace = "SeleniumTests";
		/// <summary>
		/// Gets/sets the namespace of the test fixture class. 
		/// </summary>
		public string Namespace
		{
			get { return testNamespace; }
			set { testNamespace = value; }
		}
		
		private string browser = "*firefox";
		public string Browser
		{
			get { return browser; }
			set { browser = value; }
		}
		
		private string baseFixtureType = "object";
		public string BaseFixtureType
		{
			get { return baseFixtureType; }
			set { baseFixtureType = value; }
		}
		
		private string testSuitePath = String.Empty;
		public string TestSuitePath
		{
			get { return testSuitePath; }
			set { testSuitePath = value; }
		}
		
		private string baseUrl = String.Empty;
		public string BaseUrl
		{
			get { return baseUrl; }
			set { baseUrl = value; }
		}
		
		public TestSuiteConverter ()
		{
		}
		
		public void Load(string testSuiteHtml)
		{
			TestSuiteHtml = testSuiteHtml;
		}
		
		public void LoadFile(string testSuitePath)
		{
			if (!File.Exists(testSuitePath))
				throw new ArgumentException("Cannot find test case file: " + testSuitePath, "testSuitePath");
						
			TestSuitePath = testSuitePath;
			
			using (StreamReader reader = new StreamReader(File.OpenRead(testSuitePath)))
			{
				TestSuiteHtml = reader.ReadToEnd();
			}
		}
		
		public void Convert(string outputPath)
		{
			HtmlNodeCollection nodes = new TestSuiteReader(TestSuiteHtml).ReadCases();
			
			foreach (HtmlNode node in nodes)
			{
				string title = node.InnerText;
				string file = node.Attributes["href"].Value;
				
				string filePath = Path.GetFullPath(file);
				
				if (testSuitePath != String.Empty)
					filePath = Path.Combine(Path.GetDirectoryName(testSuitePath), file);
				
				TestCaseConverter converter = new TestCaseConverter();
				
				converter.BaseFixtureType = BaseFixtureType;
				converter.Browser = Browser;
				converter.Namespace = Namespace;
				converter.BaseUrl = BaseUrl;
				
				converter.LoadFile(Path.GetFullPath(filePath));
				converter.Convert(outputPath);
			}
		
		}
	}
}

