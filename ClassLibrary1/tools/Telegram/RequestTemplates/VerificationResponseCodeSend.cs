using Telegram.Bot.Types;

namespace OTSC_ui.Tools.HTTPqUERY
{
    public class VerificationResponseCodeSend
    {
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
        public bool IsSubscribed { get; set; }

        public VerificationResponseCodeSend(string msg, int code, bool isSubscribed)
        {
            this.Message = msg;
            this.Code = code;
            this.IsSubscribed = isSubscribed;
            
        }
        public VerificationResponseCodeSend(string msg, bool isSubscribed)
        {
            this.Message = msg;
            this.IsSubscribed = isSubscribed;
            
        }
    }
}
