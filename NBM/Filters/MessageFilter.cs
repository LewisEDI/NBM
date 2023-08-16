namespace NBM.Filters;
using System.Text.RegularExpressions;

public class MessageFilter
{
    
    public string MessageType(string message)
    {
        string mType = string.Empty;

        if (message.StartsWith("e"))
        {
            mType = "email";
        }
        else if (message.StartsWith("s"))
        {
            mType = "sms";
        }
        else if (message.StartsWith("t"))
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
            Console.WriteLine("Valid message received");
        }
        else
        {
            Console.WriteLine("no match!");
        }
        
        return mType;
    }
    
   
}