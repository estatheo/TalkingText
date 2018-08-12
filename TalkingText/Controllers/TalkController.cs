using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TalkingText.Models;
using TalkingText.Services;

namespace TalkingText.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalkController : ControllerBase
    {
        public readonly ISlackClient _slackClient;
        public readonly ITalkingService _talkingService;

        public TalkController(ITalkingService talkingService, ISlackClient slackClient)
        {
            _talkingService = talkingService;
            _slackClient = slackClient;
        }

        [HttpPost, Consumes("application/x-www-form-urlencoded ")]
        public async Task<IActionResult> Post([FromForm] SlackRequest request)
        {
            if (request != null)
            {
                var spokenText = await _talkingService.TextToTalk(request.Text, "eng");
                if (spokenText != null) _slackClient.SendAudio(spokenText, request.Channel_Name, request.Text);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost, Consumes("application/x-www-form-urlencoded ")]
        [Route("ita")]
        public async Task<IActionResult> PostIta([FromForm] SlackRequest request)
        {
            if (request != null)
            {
                var spokenText = await _talkingService.TextToTalk(request.Text, "ita");
                if (spokenText != null) _slackClient.SendAudio(spokenText, request.Channel_Name, request.Text);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public void Get()
        {
            Ok();
        }
    }
}