using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TalkingText.Services
{
    public class TalkingService : ITalkingService
    {
        protected readonly IConfiguration Configuration;


        public TalkingService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<byte[]> TextToTalk(string text, string lang)
        {
            var client = new HttpClient();
            string language;
            string voice;
            switch (lang)
            {
                case "ita":
                {
                    language = "it-IT";
                    voice = "Microsoft Server Speech Text to Speech Voice (it-IT, Cosimo, Apollo)";
                    break;
                }
                default:
                {
                    language = "en-GB";
                    voice = "Microsoft Server Speech Text to Speech Voice (en-GB, HazelRUS)";
                    break;
                }
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAzureAuthToken());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ssml+xml"));
            
            client.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
            client.DefaultRequestHeaders.Add("User-Agent", "Slack TTS");
            var audioStream = await client.PostAsync("https://speech.platform.bing.com/synthesize",
                new StringContent($"<speak version=\'1.0\' xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\'{language}\'>\r\n<voice name=\'{voice}\'>\r\n   {text}\r\n</voice></speak>"));
            using (audioStream)
            {
                return await audioStream.Content.ReadAsByteArrayAsync();
            }
        }

        public async Task<string> GetAzureAuthToken()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration.GetSection("Values")["TTSCLientSecret"]);

                UriBuilder uriBuilder = new UriBuilder(Configuration.GetSection("Values")["AzureCognitiveServiceURL"]);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
