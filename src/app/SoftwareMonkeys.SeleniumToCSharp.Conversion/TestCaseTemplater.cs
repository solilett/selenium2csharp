using System;
namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	public class TestCaseTemplater
	{
		public TestCaseTemplater ()
		{
		}
		
		public string CreateTemplate()
		{
			return CreateTemplate("object");
		}
		
		public string CreateTemplate(string baseFixtureType)
		{
			string template = @"using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;
using System.Net;
using System.Net.Sockets;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace ${Namespace}
{
	[TestFixture]
	public class ${ClassName} : " + baseFixtureType + @"
	{
		private ISelenium selenium;
		private StringBuilder verificationErrors;
	
		[SetUp]
		public void Initialize()
		{
			RemoteWebDriver driver = new ${DriverName}();
			
			selenium = new WebDriverBackedSelenium(driver, ""${BaseUrl}"");
			
			selenium.Start();
			verificationErrors = new StringBuilder();
		}
		
		[TearDown]
		public void Dispose()
		{
			try
			{
				selenium.Stop();
			}
			catch (Exception)
			{
				// Ignore errors if unable to close the browser
			}
			Assert.AreEqual("""", verificationErrors.ToString());
		}
		
		[Test]
		public void Test_${TestName}()
		{
${TestCode}
		}
	}
}";
			
			return template;

		}
	}
}

