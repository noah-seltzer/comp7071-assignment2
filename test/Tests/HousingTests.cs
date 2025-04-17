using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using test.Pages;

namespace test.Tests;

public class HousingTests : BaseTest
{
    private readonly LoginPage _loginPage;

    public HousingTests()
    {
        _loginPage = new LoginPage(_driver, _baseUrl);
    }

    [Fact]
    public void HousingAccountLoginTest()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

        var emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        var passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello admin')]"));
        Assert.NotNull(userMenu);
    }

    [Fact]
    public async Task CreateNewRenter()
    {
        string testEmail = "testuser5@housing.com";

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
        Thread.Sleep(300);


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
        Thread.Sleep(300);

        emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();
        Thread.Sleep(300);


        var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello admin')]"));
        Assert.NotNull(userMenu);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Renters/Create");
        IWebElement selectElement = _driver.FindElement(By.Id("IdentityID"));
        SelectElement select = new SelectElement(selectElement);
        select.SelectByIndex(1);

        nameInput = _driver.FindElement(By.Id("Name"));
        nameInput.SendKeys("Test Renter");

        var dateOfBirthInput = _driver.FindElement(By.Id("DateOfBirth"));
        dateOfBirthInput.SendKeys("1990\t0119");

        var phoneNumberInput = _driver.FindElement(By.Id("PhoneNumber"));
        phoneNumberInput.SendKeys("1234567890");

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        submitButton = wait.Until(driver => driver.FindElement(By.Id("create-button")));

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
    public void CreateNewSuite()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

        var emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        var passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        var userMenu = _driver.FindElement(By.XPath("//a[contains(text(), 'Hello admin')]"));
        Assert.NotNull(userMenu);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Suites/Create");

        // Fill out suite details (skip LockerID, ParkingSpotID, and RenterID)
        var unitNumberInput = _driver.FindElement(By.Id("UnitNumber"));
        unitNumberInput.SendKeys("8101");

        var floorInput = _driver.FindElement(By.Id("Floor"));
        floorInput.SendKeys("1");

        var occupantsInput = _driver.FindElement(By.Id("Occupants"));
        occupantsInput.SendKeys("2");

        var roomsInput = _driver.FindElement(By.Id("Rooms"));
        roomsInput.SendKeys("3");

        var bathroomsInput = _driver.FindElement(By.Id("Bathrooms"));
        bathroomsInput.SendKeys("2");

        // Select Housing Group
        IWebElement housingGroupElement = _driver.FindElement(By.Id("HousingGroupID"));
        SelectElement housingGroupSelect = new SelectElement(housingGroupElement);
        housingGroupSelect.SelectByIndex(0);

        // Select Building
        IWebElement buildingElement = _driver.FindElement(By.Id("BuildingID"));
        SelectElement buildingSelect = new SelectElement(buildingElement);
        buildingSelect.SelectByIndex(0);

        var rentAmountInput = _driver.FindElement(By.Id("RentAmount"));
        rentAmountInput.SendKeys("1500.00");

        var isAvailableCheckbox = _driver.FindElement(By.Id("IsAvailable"));
        Thread.Sleep(500);
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", isAvailableCheckbox);
        if (!isAvailableCheckbox.Selected)
        {
            isAvailableCheckbox.Click();
        }

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var submitButton = wait.Until(driver => driver.FindElement(By.CssSelector("input[type='submit'][value='Create']")));

        // Scroll to submit button and click
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", submitButton);
        submitButton.Click();

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        // Verify that the suite was created
        var suiteEntry = _driver.FindElement(By.XPath("//td[contains(text(), '8101')]"));
        Assert.NotNull(suiteEntry);

        // Navigate to the delete page
        var deleteLink = suiteEntry.FindElement(By.XPath("./following-sibling::td/a[contains(text(), 'Delete')]"));
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteLink);
        Thread.Sleep(500);
        deleteLink.Click();
        // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(deleteLink)).Click();

        // Confirm deletion
        var confirmDeleteButton = wait.Until(driver => driver.FindElement(By.CssSelector("input[type='submit'][value='Delete']")));
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", confirmDeleteButton);
        Thread.Sleep(500);
        confirmDeleteButton.Click();

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        // Verify the suite is deleted
        var suiteList = _driver.FindElements(By.XPath($"//td[contains(text(), '8101')]"));
        Assert.Empty(suiteList); // Ensure no elements found with '8101'
    }

    [Fact]
    public void AssetDamageTest()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

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
        damageDateInput.SendKeys("2025\t0313");

        var submitButton = _driver.FindElement(By.Id("create-asset-damage"));
        submitButton.Click();

        var assetDamage = _driver.FindElement(By.XPath("//td[contains(text(), 'Test Damage')]"));
        Assert.NotNull(assetDamage);
    }

    [Fact]
    public void LockersPagesAvailableToAdmin()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

        var emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        var passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();
        Thread.Sleep(300);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Lockers");
        Thread.Sleep(300);

        var createNew = _driver.FindElements(By.XPath($"//a[contains(text(), 'Create New')]"));
        Assert.NotNull(createNew);

        var lockersTitle = _driver.FindElement(By.XPath($"//h1[contains(text(), 'Lockers')]"));
        Assert.NotNull(lockersTitle);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Lockers/Create");
        Thread.Sleep(300);

        var createTitle = _driver.FindElements(By.XPath($"//h1[contains(text(), 'Create')]"));
        Assert.NotNull(createNew);

        lockersTitle = _driver.FindElement(By.XPath($"//h4[contains(text(), 'Locker')]"));
        Assert.NotNull(lockersTitle);
    }

    [Fact]
    public void SuitesPagesAvailableToAdmin()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

        var emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        var passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Suites");
        Thread.Sleep(300);

        var createNew = _driver.FindElements(By.XPath($"//a[contains(text(), 'Create New')]"));
        Assert.NotNull(createNew);

        var suitesTitle = _driver.FindElement(By.XPath($"//h1[contains(text(), 'Suites')]"));
        Assert.NotNull(suitesTitle);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Suites/Create");
        Thread.Sleep(300);

        var createTitle = _driver.FindElements(By.XPath($"//h1[contains(text(), 'Create')]"));
        Assert.NotNull(createTitle);

        suitesTitle = _driver.FindElement(By.XPath($"//h4[contains(text(), 'Suite')]"));
        Assert.NotNull(suitesTitle);
    }

    [Fact]
    public void ParkingSpotsPagesAvailableToAdmin()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
        Thread.Sleep(300);

        var emailInput = _driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("admin@housing.com");

        var passwordInput = _driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Admin123!");

        var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/ParkingSpots");
        Thread.Sleep(300);

        var createNew = _driver.FindElements(By.XPath($"//a[contains(text(), 'Create New')]"));
        Assert.NotNull(createNew);

        var parkingSpotsTitle = _driver.FindElement(By.XPath($"//h1[contains(text(), 'ParkingSpot Spots')]"));
        Assert.NotNull(parkingSpotsTitle);

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/ParkingSpots/Create");
        Thread.Sleep(300);

        var createTitle = _driver.FindElements(By.XPath($"//h1[contains(text(), 'Create')]"));
        Assert.NotNull(createTitle);

        parkingSpotsTitle = _driver.FindElement(By.XPath($"//h4[contains(text(), 'ParkingSpot')]"));
        Assert.NotNull(parkingSpotsTitle);
    }

    [Fact]
    public void CreateInvoiceTest()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));

        // Go to create new invoice
        _driver.FindElement(By.LinkText("Housing")).Click();
        _driver.FindElement(By.LinkText("Invoices")).Click();
        _driver.FindElement(By.LinkText("Create New")).Click();

        // Select first renter (John Doe) 
        IWebElement selectRenterElement = _driver.FindElement(By.Id("RenterId"));
        SelectElement selectRenter = new SelectElement(selectRenterElement);
        selectRenter.SelectByIndex(1);

        // Select First Asset (Suite 101)
        IWebElement selectAssetElement = _driver.FindElement(By.Id("AssetId"));
        SelectElement selectAsset = new SelectElement(selectAssetElement);
        selectAsset.SelectByIndex(1);

        _driver.FindElement(By.Id("StartDate")).SendKeys("03012025");
        _driver.FindElement(By.Id("EndDate")).SendKeys("06012025");

        var submitButton = _driver.FindElement(By.Id("create-asset-invoice"));
        submitButton.Click();

        var assetInvoice = _driver.FindElement(By.XPath("//td[contains(text(), 'John Doe')]"));
        Assert.NotNull(assetInvoice);
    }
}