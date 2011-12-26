using System;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	/// <summary>
	/// Description of MockTestSuiteCreator.
	/// </summary>
	public class MockTestSuiteCreator
	{
		public string CreateHeader()
		{
			string header = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
<head>
  <meta content=""text/html; charset=UTF-8"" http-equiv=""content-type"" />
  <title>Test Suite</title>
</head>
<body>
<table id=""suiteTable"" cellpadding=""1"" cellspacing=""1"" border=""1"" class=""selenium""><tbody>
<tr><td><b>Test Suite</b></td></tr>
";
			
			return header;
		}
		
		public string CreateTestSuite(string caseTitle, string caseFile)
		{
			string testCase = CreateHeader();
			
			testCase += @"<tr><td><a href=""" + caseFile + @""">" + caseTitle + "</a></td></tr>";
			
			testCase += CreateFooter();
			
			return testCase;
		}
		
		public string CreateFooter()
		{
			string footer = @"
			</tbody>
		</table>
	</body>
</html>
";
			
			return footer;
		}
	}
}
