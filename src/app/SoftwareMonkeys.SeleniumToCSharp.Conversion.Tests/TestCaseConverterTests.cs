using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion.Tests
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class TestCaseConverterTests
	{
		[Test]
		public void Test_Convert_ToString_OpenCommand()
		{
			string mockCase = new MockTestCaseCreator().CreateTestCase("open", "TestPage.aspx");
			
			TestCaseConverter converter = new TestCaseConverter();
			converter.Load(mockCase);
			string cSharpCase = converter.Convert();
				
			throw new Exception(cSharpCase);
		}
		
	}
}