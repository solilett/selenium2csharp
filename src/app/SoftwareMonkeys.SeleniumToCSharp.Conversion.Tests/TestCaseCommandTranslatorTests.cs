using System;
using HtmlAgilityPack;
using NUnit.Framework;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	[TestFixture]
	public class TestCaseCommandTranslatorTests
	{
		[Test]
		public void Test_Translate_HtmlNode()
		{
			string target = "TestPage.html";
			
			string testCaseHtml = new MockTestCaseCreator().CreateTestCase("open", target);
			
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(testCaseHtml);
			
			HtmlNode node = doc.DocumentNode.SelectSingleNode("//html/body/table/tbody/tr");
			
			Assert.IsNotNull(node, "Couldn't find test node.");
			
			TestCaseCommandTranslator translator = new TestCaseCommandTranslator();
			string command = translator.Translate(node);
			
			string expectedCommand = "selenium.Open(\"" + target + "\");";
			
			Assert.AreEqual(expectedCommand, command, "Didn't match expected.");
		}
	}
}

