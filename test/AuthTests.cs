using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class AuthTests : IDisposable
    {
        private readonly IWebDriver _driver;
        // private readonly string _baseUrl = "http://localhost:5155";
        // private readonly string _baseUrl = "http://localhost:5000";
        private readonly string _baseUrl = "https://a2-test-bchxhed0hzgqepaw.canadacentral-01.azurewebsites.net/";
        private readonly HttpClient _httpClient;
        public AuthTests()
        {
            // This method is called before each test
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _httpClient = new HttpClient();
        }

        [Fact]
        public void LoginTest()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");

            var emailInput = _driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys("admin@housing.com");

            var passwordInput = _driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys("Admin123!");

            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello admin')]"));
            Assert.NotNull(userMenu);
        }

        private async Task DeleteTestUserAsync(string email)
        {
            var deleteUrl = $"{_baseUrl}/api/test/delete-user/{email}";
            Console.WriteLine($"Calling delete-user API: {deleteUrl}");

            var response = await _httpClient.DeleteAsync(deleteUrl);
            Console.WriteLine($"Delete response: {response.StatusCode}");
        }


        [Fact]
        public async Task RegisterNewUserTest()
        {
            string testEmail = "testuser4@housing.com";

            // Ensure user is deleted before registering
            await DeleteTestUserAsync(testEmail);

            _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Register");

            var emailInput = _driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(testEmail);

            var passwordInput = _driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys("Test123!");

            var confirmPasswordInput = _driver.FindElement(By.Id("Input_ConfirmPassword"));
            confirmPasswordInput.SendKeys("Test123!");

            var registerButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            registerButton.Click();

            // Fill in required fields
            var nameInput = _driver.FindElement(By.Id("Employee_Name"));
            nameInput.SendKeys("Test User");

            var addressInput = _driver.FindElement(By.Id("Employee_Adderess"));
            addressInput.SendKeys("123 Test Street");

            var emergencyContactInput = _driver.FindElement(By.Id("Employee_Emergency_Contact"));
            emergencyContactInput.SendKeys("123-456-7890");

            // Select first option in Job Title dropdown
            var jobTitleSelect = new SelectElement(_driver.FindElement(By.Id("Employee_Job_Title")));
            jobTitleSelect.SelectByIndex(1);  // Skip "Select job title", pick first option (Manager)

            // Select first option in Employment Type dropdown
            var employmentTypeSelect = new SelectElement(_driver.FindElement(By.Id("Employee_Employment_Type")));
            employmentTypeSelect.SelectByIndex(1);  // Skip "Select employment type", pick first option (Hourly)

            // Click Submit button
            var submitButton = _driver.FindElement(By.Id("submitButton"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", submitButton);
            submitButton.Click();

            Thread.Sleep(500);

            _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");

            emailInput = _driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(testEmail);

            passwordInput = _driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys("Test123!");

            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Wait and verify the user menu is updated (logged-in state)
            var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello testuser')]"));
            Assert.NotNull(userMenu);
        }

        public void Dispose()
        {
            // This method is called after each test
            _driver.Quit();
        }
    }
}
