using System;
using NUnit.Framework;
using HtmlAgilityPack;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	[TestFixture]
	public class TestSuiteReaderTests
	{
		public TestSuiteReaderTests ()
		{
		}
		
		[Test]
		public void Test_ReadCommands()
		{
			string mockSuiteHtml = new MockTestSuiteCreator().CreateTestSuite("Test Suite", "TestSuite.html");
			
			HtmlNodeCollection nodes = new TestSuiteReader(mockSuiteHtml).ReadCases();
			
			Assert.AreEqual(1, nodes.Count, "Invalid number of cases.");
		}
	}
}

