using System;
using NUnit.Framework;
using System.Xml;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class TestCaseReaderTests
	{
		[Test]
		public void Test_ReadCommands()
		{
			string mockCaseHtml = new MockTestCaseCreator().CreateTestCase("open", "TestPage.aspx");
			
			HtmlNodeCollection nodes = new TestCaseReader(mockCaseHtml).ReadCommands();
			
			Assert.AreEqual(1, nodes.Count, "Invalid number of commands found.");
		}
		
		[Test]
		public void Test_ReadBaseUrl()
		{
			string mockCaseHtml = new MockTestCaseCreator().CreateTestCase("open", "TestPage.aspx");
			
			string baseUrl = new TestCaseReader(mockCaseHtml).ReadBaseUrl();
			
			string expectedUrl = "http://localhost/TestApp/";
			
			Assert.AreEqual(expectedUrl, baseUrl, "Didn't read expected base URL.");
		}
		
		
		[Test]
		public void Test_ReadTestName()
		{
			string mockCaseHtml = new MockTestCaseCreator().CreateTestCase("open", "TestPage.aspx");
			
			string testName = new TestCaseReader(mockCaseHtml).ReadName();
			
			string expectedName = "TestCase";
			
			Assert.AreEqual(expectedName, testName, "Didn't read expected name.");
		}
	}
}
