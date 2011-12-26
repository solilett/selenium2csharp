/*
 * Created by SharpDevelop.
 * User: Jose
 * Date: 16/06/2011
 * Time: 7:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	/// <summary>
	/// Description of MockTestCaseCreator.
	/// </summary>
	public class MockTestCaseCreator
	{
		public string CreateHeader()
		{
			string header = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
	<head profile=""http://selenium-ide.openqa.org/profiles/test-case"">
		<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		<link rel=""selenium.base"" href=""http://localhost/TestApp/"" />
		<title>TestCase</title>
	</head>
	<body>
		<table cellpadding=""1"" cellspacing=""1"" border=""1"">
			<thead>
				<tr><td rowspan=""1"" colspan=""3"">TestCase</td></tr>
			</thead>
			<tbody>
";
			
			return header;
		}
		
		public string CreateTestCase(string command, string target, string value)
		{
			string testCase = CreateHeader();
			
			testCase += @"<tr>
	<td>" + command + @"</td>
	<td>" + target + @"</td>
	<td>" + value + @"</td>
</tr>";
			
			testCase += CreateFooter();
			
			return testCase;
		}
		
		public string CreateTestCase(string command, string target)
		{
			return CreateTestCase(command, target, String.Empty);
		}
		
		public string CreateFooter()
		{
			string footer = @"
			</tbody>
		</table>
	</body>
</html>";
			
			return footer;
		}
	}
}
