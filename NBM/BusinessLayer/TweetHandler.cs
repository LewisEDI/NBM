﻿namespace NBM.Filters;
using System.Text;

public class TweetHandler : MessageHandler
{
    public TweetHandler()
    {
        
    }

    public string tId { get; set; }
    public string tBody { get; set; }
    public List<string> tHashtags { get; set; } = new List<string>();
    public List<string> tMentions { get; set; } = new List<string>();
    
    
    public override String ProcessMessage(String message)
    {
        AbbreviationConverter Ac = new AbbreviationConverter();
        
        if (!message.StartsWith("@"))
        {
            throw new Exception("Invalid tweet, tweet must start with twitter id prefixed with @");
        }
        string[] tMsg = message.Split(new string[] { ",", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        if (tMsg[0].Length > 15)
        {
            throw new Exception("twitter ID is invalid as greater than 15 chars");
        }
        else
        {
            tId = tMsg[0];
        }
        
        StringBuilder body = new StringBuilder();
        for (int i = 1; i < tMsg.Length; i++)
        {
            body.AppendLine(tMsg[i].Trim());
        }
        if (body.Length > 140)
        {
            throw new Exception("Tweets have a maximum of 140 characters.");
        }
        else
        {
            Console.WriteLine("raw string is " + body);
            string conv = Ac.ConvertAbbreviations(body.ToString());
            tBody = conv.ToString();
            Console.WriteLine("converted string is " + conv);
        }
        
        string[] bArray = body.ToString().Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < bArray.Length; i++)
        {
            if (bArray[i].StartsWith("@"))
            {
                tMentions.Add(bArray[i].Trim());
                
            }
            else if (bArray[i].StartsWith("#"))
            {
                tHashtags.Add(bArray[i].Trim());
            }
        }
      
        Console.WriteLine("message returned is " + message);
        return message;

    }
    
    
}
