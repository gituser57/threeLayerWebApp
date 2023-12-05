using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        #region routing
        // https://learn.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2

        [HttpGet]
        // /weatherforecast
        public IEnumerable<WeatherForecast> Get()
        {
            //throw new NotImplementedException();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        // /weatherforecast/5
        public WeatherForecast Get(int id)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpGet("date")]
        // /weatherforecast/date
        public DateTime Date()
        {
            return DateTime.Now;
        }

        [HttpGet("date/{addDays}")]
        // /weatherforecast/date/5
        public DateTime Date1(int addDays = 0)
        {
            return DateTime.Now.AddDays(addDays);
        }

        [HttpGet("{addDays}/month")]
        // /weatherforecast/5/month
        public int Date2(int addDays)
        {
            return DateTime.Now.AddDays(addDays).Month;
        }

        [HttpGet("{addDays}/month/{name}")]
        // /weatherforecast/5/month/Denis
        public string Date3(int addDays, string name)
        {
            return $"Hello, {name}. That is month {DateTime.Now.AddDays(addDays).Month}";
        }
        #endregion

        #region Binding

        // https://learn.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api

        [HttpGet("orders")]
        // /WeatherForecast/orders?name=Denis&surname=Denisuk
        public string GetOrders(string name, string surname)
        {
            return $"Hello, {name} {surname}. That is month {DateTime.Now.Month}";
        }

        [HttpGet("orders/QueryParsing")]
        // /WeatherForecast/orders/QueryParsing
        public string QueryParsing()
        {
           // HttpContext.Response.ContentType = "text/html; charset=utf-8";
            var stringBuilder = new System.Text.StringBuilder("<h3>Query string parameters</h3><table>");
            stringBuilder.Append("<p><tr><td>Parameter</td><td>Value</td></tr></p>");

            foreach (var param in HttpContext.Request.Query)
            {
                stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
            }

            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        [HttpPut("orders")]
        // /WeatherForecast/orders?name=Denis
        //{
        //  "id": 5,
        //  "sum": 55.88,
        //  "customer": "BlueLaguna"
        //}

        // or [FromQuery] Order order
        // WeatherForecast/orders? name = Denis & Id = 5 & Sum = 55.88 & Customer = BlueLaguna
        public Order PutOrders(string name, Order order)
        {
            return order;
        }

        #endregion

        #region Results
        // https://metanit.com/sharp/aspnet6/10.1.php
        // app.Map("/results/{id}", SendResults);
        //  https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0

        [HttpGet("actionResults")]
        // /WeatherForecast/actionResults?name=Petro&surname=Petrenko&n=1

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetResult(string name, string surname, int n)
        { 
            if ( n == 1)
                return Content($"{name} {surname}");
            if (n == 2)
                return new JsonResult (new { Name = name, Surname = surname });
            if (n == 3)
                return Ok($"{name} {surname}");
            if (n == 4)
                return Ok(new { Name = name, Surname = surname });
            if (n == 5)
                return Redirect("/error");
            if (n == 6)
                return BadRequest("Bad request!") ;
            if (n == 7)
                NotFound(new { message = "Resource Not Found" });

            return StatusCode(500);
        }

        #endregion

        #region exception handling

        // https://learn.microsoft.com/en-us/aspnet/web-api/overview/error-handling/
        // https://metanit.com/sharp/aspnet6/9.1.php
        // https://learn.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
        // https://www.niceonecode.com/blog/84/filters-vs-middleware-in-asp.net-core
        // https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-8.0&source=recommendations !!!

        // middleware for exeption handling in Program.cs
        //    app.UseExceptionHandler(app => app.Run(async context =>
        //{
        //    context.Response.StatusCode = 500;
        //    await context.Response.WriteAsync("Error 500 occurred!");
        //}));

        [HttpGet("exceptions")]
        // /WeatherForecast/exceptions? id = 0
        public int OrderExceptions(int id)
        {
            //uncomment in Program
            //builder.Services.AddControllers(options =>
            //{
            //    options.Filters.Add<HttpResponseExceptionFilter>();
            //});
            if (id == 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No orders with ID = {0}", id)),
                    ReasonPhrase = "Order ID Not Found"
                };
                throw new HttpResponseException((int)resp.StatusCode, resp);
            }

            // uncomment in Program 
            // app.UseExceptionHandler("/Error");
            if (id == 1)
            {               
                throw new DivideByZeroException();
            }
            return 0;
        }


        #endregion
    }
}
