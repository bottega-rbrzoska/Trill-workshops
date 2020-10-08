using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Trill.Api.Controllers
{
    public class HomeController : BaseController
    {
        private readonly string _name;

        public HomeController(IOptions<ApiOptions> options)
        {
            _name = options.Value.Name;
        }

        [HttpGet]
        public ActionResult<string> Get() => _name;
    }
}