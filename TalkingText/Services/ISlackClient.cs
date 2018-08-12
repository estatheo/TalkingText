namespace TalkingText.Services
{
    public interface ISlackClient
    {
        void SendAudio(byte[] audioFile, string channels, string text);
        string Redirect();
    }
}