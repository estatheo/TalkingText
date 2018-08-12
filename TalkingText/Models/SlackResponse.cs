using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkingText.Models
{
    public class SlackResponse
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
        public string Initial_comment { get; set; }
        public string Channels { get; set; }
    }
}
