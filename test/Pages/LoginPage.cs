using OpenQA.Selenium;

namespace test.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly string _baseUrl;

    public LoginPage(IWebDriver driver, string baseUrl)
    {
        _driver = driver;
        _baseUrl = baseUrl;
    }

    public void GoToLoginPage()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Identity/Account/Login");
    }

    public void Login(string email, string password)
    {
        _driver.FindElement(By.Id("Input_Email")).SendKeys(email);
        _driver.FindElement(By.Id("Input_Password")).SendKeys(password);
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
    }

    public bool IsLoggedIn(string username)
    {
        try
        {
            return _driver.FindElement(By.XPath($"//a[contains(text(), 'Hello {username}')]")).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}