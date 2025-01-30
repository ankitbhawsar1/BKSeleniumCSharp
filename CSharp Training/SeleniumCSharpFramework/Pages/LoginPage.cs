using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCSharpFramework.Pages
{
	public class LoginPage
	{
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(driver, this);
		}

		//username
		[FindsBy(How = How.Id, Using = "username")]
		private IWebElement username;

		public IWebElement getUsernme()
		{
			return username;
		}
		//password
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        public IWebElement getPassword()
        {
            return password;
        }

        //loginbutton
        [FindsBy(How = How.CssSelector, Using = "#signInBtn")]
        private IWebElement loginBtn;

      

        public ProductPage ValidLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            loginBtn.Click();
            return new ProductPage(driver);
        }
    }
}

