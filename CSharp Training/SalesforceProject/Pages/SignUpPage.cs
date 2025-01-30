using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SalesforceProject.Pages
{
	public class SignUpPage
	{

		IWebDriver driver;

        public SignUpPage(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(driver, this);

        }

		[FindsBy(How = How.Id, Using = "UserFirstName-Ww1x")]
		private IWebElement firstName;

		[FindsBy(How = How.Id, Using = "UserLastName-wbgA")]
		private IWebElement lastName;


	}
}

