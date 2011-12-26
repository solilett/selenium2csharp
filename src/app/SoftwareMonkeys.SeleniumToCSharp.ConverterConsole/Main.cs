using System;
using SoftwareMonkeys.SeleniumToCSharp.Conversion;
using System.IO;

namespace SoftwareMonkeys.SeleniumToCSharp.ConverterConsole
{
	class MainClass
	{
		static private Arguments arguments;
		static public Arguments Arguments
		{
			get { return arguments; }
			set { arguments = value; }
		}
		
		public static void Main (string[] args)
		{
			Arguments = new Arguments(args);
			
			try
			{
				Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred...");
				Console.WriteLine(ex.ToString());
			}
		
		}
		
		private static void Run()
		{
			// Help/manual
			if (Arguments.Contains("man")
			    || Arguments.Contains("manual")
			    || Arguments.Contains("help")
			    || Arguments.Contains("h"))
				Help();
			else
			{
				if (Arguments.Contains("testcase")
			    	|| Arguments.Contains("output"))
				{
					Convert();
				}
			}
		}
		
		private static void Help()
		{
			Console.WriteLine("");
			Console.WriteLine("-------------------- Help --------------------");
			Console.WriteLine("'-help OR -man' - Display this help text.");
			Console.WriteLine("'-testcase:[path]' - Selenium test case.");
			Console.WriteLine("'-testsuite:[path]' - Selenium test suite.");
			Console.WriteLine("'-output:[folderpath]' - C# output directory.");
			Console.WriteLine("'-namespace:[namespace]' - The namespace of the C# test fixture class. (Default is 'SeleniumTests')");
			Console.WriteLine("'-basetype:[typename]' - The base class of the C# test fixture. (Default is 'object')");
			Console.WriteLine("'-browser:[browser]' - The browser to use during the test. (Default is '*firefox')");
		}
		
		private static void Convert()
		{
			
			if (!Arguments.Contains("testsuite")
			    && !Arguments.Contains("testcase"))
				throw new ArgumentException("Error: Either a test suite or test case must be specified.");
			
			if (Arguments.Contains("testsuite"))
				ConvertSuite();
			else
				ConvertCase();
						
			Console.WriteLine("-----------------------");
			Console.WriteLine("Conversion successful");
			Console.WriteLine("-----------------------");
		}
		
		private static void ConvertSuite()
		{
			Console.WriteLine("");
			Console.WriteLine("---------- Converting selenium test suite to C# ----------");
			Console.WriteLine("");
			
			string testSuitePath = ResolvePath(Arguments["testsuite"]);
			string outputPath = ResolvePath(Arguments["output"]);
			
			Console.WriteLine("Test suite path: " + testSuitePath);
			Console.WriteLine("Output path: " + outputPath);
			
			TestSuiteConverter converter = new TestSuiteConverter();
			
			if (Arguments.Contains("namespace"))
				converter.Namespace = Arguments["namespace"];
			
			if (Arguments.Contains("basetype"))
				converter.BaseFixtureType = Arguments["basetype"];
			
			if (Arguments.Contains("baseurl"))
				converter.BaseUrl = Arguments["baseurl"];
			
			if (Arguments.Contains("browser"))
				converter.Browser = Arguments["browser"];
			
			Console.WriteLine("Browser: " + converter.Browser);
			
			converter.LoadFile(testSuitePath);
			
			converter.Convert(outputPath);
		}
		
		private static void ConvertCase()
		{
			Console.WriteLine("");
			Console.WriteLine("-------------------- Converting selenium test case to C# --------------------");
			Console.WriteLine("");
			
			string testCasePath = ResolvePath(Arguments["testcase"]);
			
			string outputPath = String.Empty;
			if (Arguments.Contains("output"))
				outputPath = ResolvePath(Arguments["output"]);
			
			Console.WriteLine("Test case path: " + testCasePath);
			Console.WriteLine("Output path: " + outputPath);
			
			TestCaseConverter converter = new TestCaseConverter();
			
			if (Arguments.Contains("namespace"))
				converter.Namespace = Arguments["namespace"];
			
			if (Arguments.Contains("basetype"))
				converter.BaseFixtureType = Arguments["basetype"];
			
			if (Arguments.Contains("baseurl"))
				converter.BaseUrl = Arguments["baseurl"];
			
			if (Arguments.Contains("browser"))
				converter.Browser = Arguments["browser"];
			
			
			converter.LoadFile(testCasePath);
			
			converter.Convert(outputPath);
		}
		
		private static string ResolvePath(string path)
		{
			return Path.GetFullPath(path);
		}
	}
}

