namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;
    using System.Collections.Generic;

    public class CalculatorContoller : Controller
    {
        public IHttpResponse Calculator()
        {
            return this.FileViewResponse(@"calculator\calculator", new Dictionary<string, string>()
            {
                ["displayForm"] = "none"
            });
        }

        public IHttpResponse Calculator(string firstValue, string sign, string secondValue)
        {
            var result = "Invalid Sign";

            var valueOne = decimal.Parse(firstValue);
            var valueTwo = decimal.Parse(secondValue);

            switch (sign)
            {
                case "+":
                    result = (valueOne + valueTwo).ToString();
                    break;
                case "-":
                    result = (valueOne - valueTwo).ToString();
                    break;
                case "*":
                    result = (valueOne * valueTwo).ToString();
                    break;
                case "/":
                    result = (valueOne / valueTwo).ToString();
                    break;
            }

            return this.FileViewResponse(@"calculator\calculator", new Dictionary<string, string>()
            {
                ["result"] = result,
                ["displayForm"] = "display"
            });
        }
    }
}
