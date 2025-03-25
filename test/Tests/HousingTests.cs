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
    public void CreateNewRenter()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));

        _driver.Navigate().GoToUrl($"{_baseUrl}/Housing/Renters/Create");
    
        new SelectElement(_driver.FindElement(By.Id("IdentityID"))).SelectByIndex(1);
    
        _driver.FindElement(By.Id("Name")).SendKeys("Test Renter");
        _driver.FindElement(By.Id("DateOfBirth")).SendKeys("1990\t0119");
        _driver.FindElement(By.Id("PhoneNumber")).SendKeys("1234567890");

        WaitAndClick(By.Id("create-button"));

        var renter = _driver.FindElement(By.XPath("//td[contains(text(), 'Test Renter')]"));
        Assert.NotNull(renter);
    }
    
    [Fact]
    public void CreateNewSuite()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));

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
    public void CreateAssetDamageTest()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));
        
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
    public void CreateInvoiceTest()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));
        
        // Go to create new invoice
        Thread.Sleep(500);
        _driver.FindElement(By.LinkText("Housing")).Click();
        Thread.Sleep(500);
        _driver.FindElement(By.LinkText("Invoices")).Click();
        Thread.Sleep(500);
        _driver.FindElement(By.LinkText("Create New")).Click();
        Thread.Sleep(500);

        // Select first renter (John Doe) 
        IWebElement selectRenterElement = _driver.FindElement(By.Id("RenterId"));
        SelectElement selectRenter = new SelectElement(selectRenterElement);
        selectRenter.SelectByIndex(1);

        // Select First Asset (Suite 101)
        IWebElement selectAssetElement = _driver.FindElement(By.Id("AssetId"));
        SelectElement selectAsset = new SelectElement(selectAssetElement);
        selectAsset.SelectByIndex(1);
        
        Thread.Sleep(500);
        _driver.FindElement(By.Id("StartDate")).SendKeys("03012025");
        Thread.Sleep(500);
        _driver.FindElement(By.Id("EndDate")).SendKeys("06012025");
        Thread.Sleep(1000);
        
        var submitButton = _driver.FindElement(By.Id("create-asset-invoice"));
        submitButton.Click();
        
        var assetInvoice = _driver.FindElement(By.XPath("//td[contains(text(), 'John Doe')]"));
        Thread.Sleep(1000);
        Assert.NotNull(assetInvoice);
    }
}