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
        private readonly string _baseUrl = "http://localhost:5155";
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

            var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello testuser')]"));
            Assert.NotNull(userMenu);
        }

        [Fact]
        public void CreateNewRenter()
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

            _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Renters/Create");
            IWebElement selectElement = _driver.FindElement(By.Id("IdentityID"));
            SelectElement select = new SelectElement(selectElement);
            select.SelectByIndex(1);
            
            var nameInput = _driver.FindElement(By.Id("Name"));
            nameInput.SendKeys("Test Renter");

            var dateOfBirthInput = _driver.FindElement(By.Id("DateOfBirth"));
            dateOfBirthInput.SendKeys("1990\t0119");

            var phoneNumberInput = _driver.FindElement(By.Id("PhoneNumber"));
            phoneNumberInput.SendKeys("1234567890");

            var emailInput2 = _driver.FindElement(By.Id("Email"));
            emailInput2.SendKeys("testEmaiul5@test.com");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var submitButton = wait.Until(driver => driver.FindElement(By.Id("create-button")));
            /**
            * Scroll to the submit button before clicking it.
            * This is necessary because the button is not clickable if it is not on the screen.
            */
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", submitButton);
            submitButton.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            var renter = _driver.FindElement(By.XPath("//td[contains(text(), 'Test Renter')]"));
            Assert.NotNull(renter);
        }
        
        [Fact]
        public void AssetDamageTest()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");

            var emailInput = _driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys("admin@housing.com");

            var passwordInput = _driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys("Admin123!");

            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();
            
            // Go to create new asset damage
            Thread.Sleep(500);
            _driver.FindElement(By.LinkText("Housing")).Click();
            Thread.Sleep(500);
            _driver.FindElement(By.LinkText("Assets")).Click();
            Thread.Sleep(500);
            _driver.FindElement(By.LinkText("Asset Damages")).Click();
            Thread.Sleep(500);
            _driver.FindElement(By.LinkText("Create New")).Click();
            Thread.Sleep(500);
            
            // Fill out create new form
            IWebElement selectAssetElement = _driver.FindElement(By.Id("AssetID"));
            SelectElement selectAsset = new SelectElement(selectAssetElement);
            selectAsset.SelectByIndex(1);
            
            IWebElement selectRenterElement = _driver.FindElement(By.Id("RenterID"));
            SelectElement selectRenter = new SelectElement(selectRenterElement);
            selectRenter.SelectByIndex(1);
            
            var descriptionInput = _driver.FindElement(By.Id("Description"));
            descriptionInput.SendKeys("Test Damage");

            var damageDateInput = _driver.FindElement(By.Id("RecordedDate"));
            damageDateInput.SendKeys("2025\0313");

            var submitButton = _driver.FindElement(By.Id("create-asset-damage"));
            submitButton.Click();
            
            var assetDamage = _driver.FindElement(By.XPath("//td[contains(text(), 'Test Damage')]"));
            Assert.NotNull(assetDamage);
        }

        public void Dispose()
        {
            // This method is called after each test
            _driver.Quit();
        }
    }
}
