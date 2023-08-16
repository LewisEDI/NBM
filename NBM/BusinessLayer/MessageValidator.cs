namespace NBM.Filters;
using System.Text.RegularExpressions;

using System;
using System.Text.RegularExpressions;

class MessageValidator
{
    
    public MessageHandler ValidateMessage(string header, string message)
    {
        MessageHandler messageHandler = null;
        string mType = string.Empty;
        string validationMessage = string.Empty;

        if (header.StartsWith("e"))
        {
            messageHandler = new EmailHandler();
        }
        else if (header.StartsWith("s"))
        {
            messageHandler = new SMSHandler();
        }
        else if (header.StartsWith("t"))
        {
            mType = "tweet";
        }
        else
        {
            throw new Exception("Invalid message type, message received is " + message);
        }

        string pattern = @"^[1-9]{9}$";
        string nextNineChars = message[1..];
        bool ninesValidation = Regex.IsMatch(nextNineChars, pattern);

        if (ninesValidation)
        {
        }
        else
        {
            //Throw exception
            validationMessage = "No match!";
        }

        return messageHandler;
    }
}


