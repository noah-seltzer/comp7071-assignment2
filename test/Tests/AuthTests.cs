using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using test.Pages;

namespace test.Tests;

public class AuthTests : BaseTest
{
    private readonly LoginPage _loginPage;

    public AuthTests()
    {
        _loginPage = new LoginPage(_driver, _baseUrl);
    }

    [Fact]
    public void LoginTest()
    {
        _loginPage.GoToLoginPage();
        _loginPage.Login("admin@housing.com", "Admin123!");
        Assert.True(_loginPage.IsLoggedIn("admin"));
    }

    [Fact]
    public async Task RegisterNewUserTest()
    {
        string testEmail = "testuser4@housing.com";
        string testName = "Test User";

        await RegisterNewUser(
            testEmail,
            "Test123!",
            testName,
            "123 Test Street",
            "123-456-7890"
        );

        _loginPage.GoToLoginPage();
        _loginPage.Login(testEmail, "Test123!");
        Assert.True(_loginPage.IsLoggedIn("testuser"));
    }
}