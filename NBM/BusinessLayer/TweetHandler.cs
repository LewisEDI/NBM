using System.Runtime.InteropServices.JavaScript;

namespace NBM.Filters;
using System.Text;

/*
public class TweetHandler : MessageHandler
{

    public string tId { get; set; }
    public string tBody { get; set; }
    public List<string> tHashtags { get; set; } = new List<string>();
    public List<string> tMentions { get; set; } = new List<string>();
    
    
    public override String ProcessMessage(String message)
    {
        string modBody = String.Empty;
        AbbreviationConverter Ac = new AbbreviationConverter();
        
        if (!message.StartsWith("@"))
        {
            throw new Exception("Invalid tweet, tweet must start with twitter id prefixed with @");
        }
        //string[] tMsg = message.Split(new string[] { ",", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        string[] tMsg = message.Split(new string[] { ","}, StringSplitOptions.RemoveEmptyEntries);
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
            modBody = conv;
            tBody = modBody;
            Console.WriteLine("converted string is " + conv);
        }
        
        //string[] bArray = body.ToString().Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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
      
        Console.WriteLine("message returned is " + modBody);
        
        return modBody;

    }
    
    
}
*/
 public class TweetHandler : MessageHandler
    {
        public string tId { get; set; }
        public string tBody { get; set; }
        public List<string> tHashtags { get; set; } = new List<string>();
        public List<string> tMentions { get; set; } = new List<string>();

        public override string ProcessMessage(string header, string message)
        {
            string modBody = string.Empty;
            AbbreviationConverter Ac = new AbbreviationConverter();

            if (!message.StartsWith("@"))
            {
                throw new Exception("Invalid tweet, tweet must start with a Twitter handle prefixed with @");
            }

            int bodyStartIndex = message.IndexOf(",");
            if (bodyStartIndex == -1)
            {
                throw new Exception("Invalid tweet format");
            }

            tId = message.Substring(0, bodyStartIndex).Trim();
            string body = message.Substring(bodyStartIndex + 1).Trim();

            if (tId.Length > 15)
            {
                throw new Exception("Twitter handle is invalid as it exceeds 15 characters");
            }

            if (body.Length > 140)
            {
                throw new Exception("Tweets have a maximum of 140 characters.");
            }

            Console.WriteLine("Raw tweet body: " + body);
            modBody = Ac.ConvertAbbreviations(body);
            tBody = modBody;
            Console.WriteLine("Converted tweet body: " + modBody);

            string[] words = modBody.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word.StartsWith("#"))
                {
                    tHashtags.Add(word.Trim());
                }
                else if (word.StartsWith("@"))
                {
                    tMentions.Add(word.Trim());
                }
            }

            Console.WriteLine("Tweet Handle: " + tId);
            Console.WriteLine("Tweet Body: " + tBody);
            Console.WriteLine("Hashtags: " + string.Join(", ", tHashtags));
            Console.WriteLine("Mentions: " + string.Join(", ", tMentions));

            return modBody;
        }

        public List<List<string>> GetTweetLists()
        {
            List<List<string>> tLists= new List<List<string>>();
                
            List<string> mentionsList = tMentions;
            List<string> hashtagList = tHashtags;
            
            tLists.Add(mentionsList);
            tLists.Add(hashtagList);

            return tLists;
        }
    }







