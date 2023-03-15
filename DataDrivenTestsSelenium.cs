using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataDrivenWebDriverTests
{
    public class DataDrivenTestsSelenium
    {
        private WebDriver driver;
        private const string baseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        IWebElement firstInput;
        IWebElement secondInput;
        IWebElement operationField;
        IWebElement calcButton;
        IWebElement resultField;
        IWebElement resetButton;

        [OneTimeSetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);

           firstInput = driver.FindElement(By.Id("number1"));
           secondInput = driver.FindElement(By.Id("number2"));
           operationField = driver.FindElement(By.Id("operation"));
           calcButton = driver.FindElement(By.Id("calcButton"));
           resultField = driver.FindElement(By.Id("result"));
           resetButton = driver.FindElement(By.Id("resetButton"));
        }

        [OneTimeTearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }

        [TestCase ("10", "+", "9", "Result: 19")]
        [TestCase("-10", "+", "-9", "Result: -19")]
        [TestCase("10", "*", "-9", "Result: -90")]
        [TestCase("90", "/", "-9", "Result: -10")]
        [TestCase("-90", "+", "-9", "Result: -99")]
        [TestCase("-90", "+", "hello", "Result: invalid input")]
        [TestCase("8.99", "/", "10", "Result: 0.899")]
        [TestCase("8.99", "+", "10", "Result: 18.99")]
        [TestCase("8.99", "*", "10", "Result: 89.9")]

        public void TestCalculatorFunctions(string firstNum, string operation, string secondNum, string expectedResult)
        {
            resetButton.Click();
            firstInput.SendKeys(firstNum);
            operationField.SendKeys(operation);
            secondInput.SendKeys(secondNum);
            calcButton.Click();

            Assert.That(expectedResult, Is.EqualTo(resultField.Text));
        }
    }
}