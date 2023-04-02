using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Luiza.Labs.Tests.Automated.User
{
    public static class Login
    {
        public static void loginWithSuccess(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            IWebElement emailAddress = driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/form/div[1]/input"));
            emailAddress.SendKeys("gu_conta_de_teste@outlook.com");
            IWebElement password = driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/form/div[2]/input"));
            password.SendKeys("123456");

            IWebElement btnSend = driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/form/div[3]/div/div[1]/button"));

            btnSend.Click();

            driver.Quit();
        }
    }
}
