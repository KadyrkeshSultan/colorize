using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Colorize.Models;
using Newtonsoft.Json.Linq;

namespace Colorize.Controllers
{
    public class HomeController : Controller
    {
        public Algorithmia.Client client;
        public HomeController(ClientService _client)
        {
            client = _client.GetClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Color(string url)
        {
            try
            {
                var input = "{ " + "\"image\" :\"" + url + "\"}";
                var algorithm = client.algo("deeplearning/ColorfulImageColorization/1.1.13");
                var response = algorithm.pipeJson<object>(input);
                JObject res = (JObject)response.result;
                string output = (string)res["output"];
                var file = client.file(output).getBytes();
                return new OkObjectResult(file);
            }
            catch(Exception exp)
            {
                return new BadRequestObjectResult(exp.Message);
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
