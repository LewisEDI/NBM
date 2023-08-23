namespace NBM.Filters;
using System.Text.RegularExpressions;

using System;
using System.Text.RegularExpressions;

class MessageFilter
{
    //create handlers
    //maybe refactor this later to create single handler dependent on header. 
    public TweetHandler TH = new TweetHandler();
    public EmailHandler EH = new EmailHandler();
    public SMSHandler SMSH = new SMSHandler();
    
    public EmailHandler GetEH() => EH;
    public TweetHandler GetTH() => TH;

    public MessageHandler ValidateMessage(string header, string message)
    {
        MessageHandler messageHandler = null;
        
        if (header.StartsWith("e"))
        {
            messageHandler = EH;
        }
        else if (header.StartsWith("s"))
        {
            messageHandler = SMSH;
        }
        else if (header.StartsWith("t"))
        {
            messageHandler = TH;
        }
        else
        {
            throw new Exception("Invalid message type, message received is " + message);
        }

        string pattern = @"^[1-9]{9}$";
        string nextNineChars = header[1..];
        bool ninesValidation = Regex.IsMatch(nextNineChars, pattern);

        if (!ninesValidation)
        {
            throw new Exception("Invalid message ID, message received is " + message);
        }

        return messageHandler;
    }
}


