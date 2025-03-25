using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace test
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly IWebDriver _driver;
        protected readonly string _baseUrl = "http://localhost:5155";
        protected readonly HttpClient _httpClient;
        
        protected BaseTest()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _httpClient = new HttpClient();
        }

        protected async Task RegisterNewUser(string email, string password, string name, string address, string emergencyContact)
        {
            await DeleteTestUserAsync(email);

            _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Register");

            // Fill registration form
            _driver.FindElement(By.Id("Input_Email")).SendKeys(email);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(password);
            _driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys(password);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Fill employee details
            _driver.FindElement(By.Id("Employee_Name")).SendKeys(name);
            _driver.FindElement(By.Id("Employee_Adderess")).SendKeys(address);
            _driver.FindElement(By.Id("Employee_Emergency_Contact")).SendKeys(emergencyContact);

            new SelectElement(_driver.FindElement(By.Id("Employee_Job_Title"))).SelectByIndex(1);
            new SelectElement(_driver.FindElement(By.Id("Employee_Employment_Type"))).SelectByIndex(1);

            var submitButton = _driver.FindElement(By.Id("submitButton"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", submitButton);
            submitButton.Click();
        }

        protected async Task DeleteTestUserAsync(string email)
        {
            var deleteUrl = $"{_baseUrl}/api/test/delete-user/{email}";
            var response = await _httpClient.DeleteAsync(deleteUrl);
            Console.WriteLine($"Delete response: {response.StatusCode}");
        }

        protected void WaitAndClick(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            var element = wait.Until(d => d.FindElement(locator));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", element);
            element.Click();
        }

        public void Dispose()
        {
            _driver.Quit();
            _httpClient.Dispose();
        }
    }
}