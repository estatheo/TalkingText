using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalkingText.Services;

namespace TalkingText.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallController : ControllerBase
    {
        public readonly ISlackClient _slackClient;

        public InstallController(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Redirect(new Uri(_slackClient.Redirect()).AbsoluteUri);
        }
    }
}