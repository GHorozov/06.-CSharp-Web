namespace _01.FirstWebSite_ByTheCakeApp.Application.Controllers
{
    using System;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;

    public class CalculatorController
    {
        public HttpResponse CalculatorGet()
        {
            return new ViewResponse(HttpResponceCode.OK, new CalculatorView());
        }

        public HttpResponse CalculatorPost(string valueOne, string sign, string valueTwo)
        {
            var calculatorView = new CalculatorView();
            try
            {
                var firstValue = decimal.Parse(valueOne);
                var secondValue = decimal.Parse(valueTwo);

                if(sign != "+" && sign != "-" && sign != "*" && sign != "/")
                {
                    calculatorView.ErrorString = "Invalid Sign!";
                    return new ViewResponse(HttpResponceCode.OK, calculatorView);
                }

                decimal currentResult;
                switch (sign.Trim())
                {
                    case "+":
                        currentResult = firstValue + secondValue;
                        break;
                    case "-":
                        currentResult = firstValue - secondValue;
                        break;
                    case "*":
                        currentResult = firstValue * secondValue;
                        break;
                    case "/":
                        currentResult = firstValue / secondValue;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                calculatorView.ErrorString = currentResult.ToString();
            }
            catch (Exception)
            {
                calculatorView.ErrorString = "Invalid expression!";
            }

            return new ViewResponse(HttpResponceCode.OK, calculatorView);
        }
    }
}
