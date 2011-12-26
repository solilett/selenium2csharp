/*
 * Created by SharpDevelop.
 * User: Jose
 * Date: 16/06/2011
 * Time: 7:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	/// <summary>
	/// Converts a selenium test case file into a C# file.
	/// </summary>
	public class TestCaseConverter
	{
		private string testCaseHtml;
		/// <summary>
		/// Gets/sets the test case HTML.
		/// </summary>
		public string TestCaseHtml
		{
			get { return testCaseHtml; }
			set { testCaseHtml = value; }
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
		
		private string baseUrl = String.Empty;
		public string BaseUrl
		{
			get { return baseUrl; }
			set { baseUrl = value; }
		}
		
		private string testName = String.Empty;
		public string TestName
		{
			get { return testName; }
		}
		
		public TestCaseConverter()
		{
		}
		
		#region Main functions
		public void Load(string testCaseHtml)
		{
			TestCaseHtml = testCaseHtml;
		}
		
		public void LoadFile(string testCasePath)
		{
			if (!File.Exists(testCasePath))
				throw new ArgumentException("Cannot find test case file: " + testCasePath, "testCasePath");
						
			using (StreamReader reader = new StreamReader(File.OpenRead(testCasePath)))
			{
				TestCaseHtml = reader.ReadToEnd();
			}
		}
		
		public string Convert()
		{
			TestCaseReader reader = new TestCaseReader(TestCaseHtml);
			
			HtmlNodeCollection commandNodes = reader.ReadCommands();
			
			testName = reader.ReadName();
			
			string baseUrl = reader.ReadBaseUrl();
			
			// If a base URL was specified then use it instead of the one in the actual test
			if (BaseUrl != String.Empty)
				baseUrl = BaseUrl;
			
			string template = new TestCaseTemplater().CreateTemplate(BaseFixtureType);
			
			string commandsCode = new TestCaseCommandTranslator().Translate(commandNodes);
			
			string output = template;
			
			output = output.Replace("${TestCode}", commandsCode);
			output = output.Replace("${BaseUrl}", baseUrl);
			output = output.Replace("${TestName}", TestName.Replace("-", "_"));
			output = output.Replace("${Namespace}", Namespace);
			output = output.Replace("${ClassName}", TestName.Replace("-", "_") + "TestFixture_" + Browser.Trim('*'));
			output = output.Replace("${Browser}", Browser);
			output = output.Replace("${DriverName}", GetDriverName(Browser));
			
			return output;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputPath">
		/// The folder to output the file to.
		/// </param>
		public void Convert(string outputFolderPath)
		{
			string output = Convert();
			
			if (!Directory.Exists(outputFolderPath))
				Directory.CreateDirectory(outputFolderPath);
			
			string outputPath = Path.Combine(outputFolderPath, TestName + "TestFixture_" + Browser.Trim('*') + ".cs");
			
			using (StreamWriter writer = File.CreateText(outputPath))
			{
				writer.Write(output);
			}
		}
		
		public string GetDriverName(string browser)
		{
			switch (browser.Trim('*'))
			{
			case "iexplore":
				return "OpenQA.Selenium.IE.InternetExplorerDriver";
			case "firefox":
				return "OpenQA.Selenium.Firefox.FirefoxDriver";
			case "chrome":
				return "OpenQA.Selenium.Chrome.ChromeDriver";
			default:
				throw new NotImplementedException("No driver has been implemented for the '" + browser + "' browser.");
			}
		}
		#endregion
		
		
	}
}
