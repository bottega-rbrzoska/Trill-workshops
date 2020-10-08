using Microsoft.AspNetCore.Mvc;

namespace Trill.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}