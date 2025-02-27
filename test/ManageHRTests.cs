using Microsoft.AspNetCore.Routing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;
namespace test
{
    public class ManageHRTest
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "https://localhost:7203/ManageHumanResourcesAndPayroll/HREmployees";
        private readonly HttpClient _httpClient;
        public ManageHRTest()
        {
            // This method is called before each test
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _httpClient = new HttpClient();
        }

        [Fact]
        public void TestCreateLockout()
        {
            //ChromeDriver driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl($"{_baseUrl}");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.FindElement(By.LinkText("Create New")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.FindElement(By.Id("Name")).SendKeys("Marge Simpson");
            _driver.FindElement(By.Id("Adderess")).SendKeys("123 ABC Street");
            _driver.FindElement(By.Id("Emergency_Contact")).SendKeys("123-456-7890");

            // select the drop down list
            var job_title_list = _driver.FindElement(By.Name("Job_Title"));
            //create select element object 
            var selectElement = new SelectElement(job_title_list);
            //select by value
            selectElement.SelectByValue("Manager");

            // select the drop down list
            var employment_type_list = _driver.FindElement(By.Name("Employment_Type"));
            //create select element object 
            var selectElement2 = new SelectElement(employment_type_list);
            //select by value
            selectElement2.SelectByValue("hourly");
            _driver.FindElement(By.XPath("//Input[@type='submit']")).Click();
            _driver.FindElements(By.XPath("//li[text()='Only managers can create new employees.']"));
        }
    }
}