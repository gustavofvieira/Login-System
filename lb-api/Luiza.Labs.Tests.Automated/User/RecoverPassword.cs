using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Tests.Automated.User
{
    public static class RecoverPassword
    {
        public static void recoverPasswordWithSuccess(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            IWebElement emailAddress = driver.FindElement(By.XPath("/html/body/app-root/app-recover-pass/div/div/div/form/div[1]/input"));
            emailAddress.SendKeys("gu_conta_de_teste@outlook.com");
            IWebElement btnSend = driver.FindElement(By.XPath("/html/body/app-root/app-recover-pass/div/div/div/form/div[2]/div/div/button"));

            btnSend.Click();

            driver.Quit();
        }
    }
}
