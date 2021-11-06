using Microsoft.AspNetCore.Mvc;
using Northvolt.Models;
using System.Collections.Generic;

namespace Northvolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValuePairController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> Get() => KeyValuePairModel.GetList();

        [HttpPost]
        public string Add(string key, double value) => KeyValuePairModel.Add(key, value);

        [HttpDelete]
        public string Prune() => KeyValuePairModel.Prune();
    }
}
