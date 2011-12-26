using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SoftwareMonkeys.SeleniumToCSharp.Conversion
{
	public class TestCaseCommandTranslator
	{
		public TestCaseCommandTranslator ()
		{
		}

		public string Translate (HtmlNodeCollection commandNodes)
		{
			string output = String.Empty;
			
			foreach (HtmlNode node in commandNodes) {
				output += "\t\t\t"; // three tabs should indent the code appropriately
				output += Translate (node);
				output += "\n";
			}
			
			return output;
		}

		public string Translate (HtmlNode commandNode)
		{
			HtmlNodeCollection cellNodes = commandNode.SelectNodes ("td");
			
			string command = cellNodes[0].InnerText;
			string target = cellNodes[1].InnerText;
			string value = cellNodes[2].InnerText;
			
			return Translate (command, target, value);
		}

		public string Translate (string command, string target, string value)
		{
			string commandCode = String.Empty;
			
			switch (command) {
			case "addSelection":
				commandCode = "selenium.AddSelection(\"" + Escape(target) + "\", \"" + Escape(value) + "\");";
				break;
			case "assertConfirmation":
				commandCode = "Assert.IsTrue(selenium.GetConfirmation() != null && selenium.GetConfirmation().IndexOf(\"" + Escape(target) + "\") > -1, \"Confirm box didn't appear when expected.\");";
				break;
			case "assertSelectedLabel":
				commandCode = "Assert.AreEqual(\"" + Escape(value) + "\", selenium.GetSelectedLabel(\"" + Escape(target) + "\"));";
				break;
			case "assertSelectedLabels":
				commandCode = "Assert.AreEqual(\"" + Escape(value) + "\", String.Join(\",\", selenium.GetSelectedLabels(\"" + Escape(target) + "\")));";
				break;
			case "assertTextPresent":
				commandCode = "Assert.IsTrue(selenium.IsTextPresent(\"" + Escape(target) + "\"), \"Text '" + Escape(target) + "' not found when it should be.\");";
				break;
			case "assertTextNotPresent":
				commandCode = "Assert.IsFalse(selenium.IsTextPresent(\"" + Escape(target) + "\"), \"Text '" + Escape(target) + "' found when it shouldn't be.\");";
				break;
			case "check":
				commandCode = "if (!selenium.IsChecked(\"" + Escape(target) + "\"))\n" +
			"\t\t\tselenium.Click(\"" + Escape(target) + "\");";
				break;
			case "chooseCancelOnNextConfirmation":
				commandCode = "selenium.ChooseCancelOnNextConfirmation();";
				break;
			case "chooseOkOnNextConfirmation":
				commandCode = "selenium.ChooseOkOnNextConfirmation();";
				break;
			case "click":
				commandCode = "selenium.Click(\"" + Escape(target) + "\");";
				break;
			case "clickAndWait":
				commandCode = "selenium.Click(\"" + Escape(target) + "\");\n" +
				"\t\t\tselenium.WaitForPageToLoad(\"30000\");";
				break;
			case "close":
				commandCode = "selenium.Close();";
				break;
			case "open":
				commandCode = "selenium.Open(\"" + Escape(target) + "\");";
				break;
			case "pause":
				commandCode = "Thread.Sleep(" + Escape(target) + ");";
				break;
			case "removeSelection":
				commandCode = "selenium.RemoveSelection(\"" + Escape(target) + "\", \"" + Escape(value) + "\");";
				break;
			case "setTimeout":
				commandCode = "selenium.SetTimeout(\"" + Escape(target) + "\");";
				break;
			case "selectAndWait":
				commandCode = "selenium.AddSelection(\"" + Escape(target) + "\", \"" + Escape(value) + "\");\n" +
				"\t\t\tselenium.WaitForPageToLoad(\"30000\");";
				break;
			case "select":
				commandCode = "selenium.AddSelection(\"" + Escape(target) + "\", \"" + Escape(value) + "\");\n";
				break;
			case "type":
				commandCode = "selenium.Type(\"" + Escape(target) + "\", \"" + Escape(value) + "\");";
				break;
			case "uncheck":
				commandCode = "if (selenium.IsChecked(\"" + Escape(target) + "\"))\n" +
				"\t\t\tselenium.Click(\"" + Escape(target) + "\");";
				break;
			case "verifyTextPresent":
				commandCode = "try\n" +
			"\t\t\t{\n" +
			"\t\t\t\tAssert.IsTrue(selenium.IsTextPresent(\"" + Escape(value) + "\"));\n" +
			"\t\t\t}\n" +
			"\t\t\tcatch (AssertionException e)\n" +
			"\t\t\t{\n" +
			"\t\t\t\tverificationErrors.Append(e.Message);\n" +
			"\t\t\t}\n";
				break;
			case "verifyTextNotPresent":
				commandCode = "try\n" +
			"\t\t\t{\n" +
			"\t\t\t\tAssert.IsFalse(selenium.IsTextPresent(\"" + Escape(value) + "\"));\n" +
			"\t\t\t}\n" +
			"\t\t\tcatch (AssertionException e)\n" +
			"\t\t\t{\n" +
			"\t\t\t\tverificationErrors.Append(e.Message);\n" +
			"\t\t\t}\n";
				break;
			case "waitForPageToLoad":
				commandCode = "selenium.WaitForPageToLoad(\"30000\");";
				break;
			case "waitForTextPresent":
				commandCode = "while (!selenium.IsTextPresent(\"" + Escape(target) + "\"))\n" +
				"\t\t\tThread.Sleep(1000);";
				break;
			case "waitForTextNotPresent":
				commandCode = "while (!selenium.IsTextPresent(\"" + Escape(target) + "\"))\n" +
				"\t\t\tThread.Sleep(1000);";
				break;
			}
			
			if (commandCode == String.Empty)
				throw new NotImplementedException ("Translation of the command '" + command + "' has not yet been implemented.");
			
			return commandCode;
		}
		
		public string Escape(string original)
		{
			string output = original;
			output = output.Replace(@"""", @"\""");
			output = output.Replace(@"\", @"\\");
			return output;
		}
	}
}

