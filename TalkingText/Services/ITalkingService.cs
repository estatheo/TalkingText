using System.Threading.Tasks;

namespace TalkingText.Services
{
    public interface ITalkingService
    {
        Task<byte[]> TextToTalk(string text, string lang);
    }
}