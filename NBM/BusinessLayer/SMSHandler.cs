namespace NBM.Filters;

public class SMSHandler : MessageHandler
{
    
    public string smsSender { get; set; }
    public string smsBody { get; set; }
    public override String ProcessMessage(String header, String message)
    {
        AbbreviationConverter smsAc = new AbbreviationConverter();

        const string senderTag = "Sender:";
        const string messageTag = "Message:";

        int senderIndex = message.IndexOf(senderTag, StringComparison.OrdinalIgnoreCase);
        int messageIndex = message.IndexOf(messageTag, StringComparison.OrdinalIgnoreCase);

        if (senderIndex == -1 || messageIndex == -1)
        {
            return "Missing required tags: Sender or Message";
        }

        string senderValue = GetTagValue(senderTag, message, senderIndex);
        string messageValue = GetTagValue(messageTag, message, messageIndex);

        if (!senderValue.StartsWith("+"))
        {
            return "valid international mobile number must start with '+'";
        }
        else
        {
            smsSender = senderValue;
        }

        if (messageValue.Length > 140)
        {
            return "SMS message must not be greater than 140 chars";
        }
        else
        {
            smsBody = smsAc.ConvertAbbreviations(messageValue);
        }
        

        return $"Sender: {senderValue}\nMessage: {messageValue}";
    }

    private string GetTagValue(string tag, string message, int startIndex)
    {
        int tagIndex = startIndex + tag.Length;
        int nextTagIndex = message.IndexOfAny(new char[] { '\n', '\r' }, tagIndex);
        if (nextTagIndex == -1)
        {
            return message.Substring(tagIndex).Trim();
        }
        return message.Substring(tagIndex, nextTagIndex - tagIndex).Trim();
    }
    
}