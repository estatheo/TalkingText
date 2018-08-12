using System.IO;
using Microsoft.AspNetCore.Mvc;
using TalkingText.Models;
using TalkingText.Services;

namespace TalkingText.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        public readonly ISlackClient _slackClient;

        public SongController(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        [HttpPost, Consumes("application/x-www-form-urlencoded ")]
        public void Post([FromForm] SlackRequest request)
        {
            if (request != null)
            {
                var spokenText = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\Assets\\despacito.mp3");
                if (spokenText != null) _slackClient.SendAudio(spokenText, request.Channel_Name, "despascito");
                Ok();
            }
            BadRequest();
        }
        
        [HttpGet]
        public void Get()
        {
            Ok();
        }
    }
}