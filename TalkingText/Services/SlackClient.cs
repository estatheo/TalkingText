using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TalkingText.Models;

namespace TalkingText.Services
{
    public class SlackClient : ISlackClient
    {
        private RestClient _client;
        public readonly IConfiguration Configuration;

        public SlackClient(IConfiguration configuration)
        {
            Configuration = configuration;
            _client = new RestClient("https://slack.com");
        }

        public void SendAudio(byte[] audioFile, string channels, string text)
        {
            var request = new RestRequest("api/files.upload", Method.POST);
            request.AddParameter("token", Configuration.GetSection("Values")["SlackBotToken"]);
            request.AddParameter("channels", channels);
            var title = text.Replace('.', ' ');
            request.AddParameter("filename", $"{text}.mp3");
            request.AddFile("file", audioFile, $"{text}.mp3", contentType: "multipart/form-data");
            var response = _client.Execute(request);
            var content = response.Content;
            Console.Error.WriteLineAsync(content);
        }
    }
}
