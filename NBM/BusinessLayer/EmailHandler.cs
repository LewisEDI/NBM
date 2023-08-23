namespace NBM.Filters;

public class EmailHandler : MessageHandler
{
    public EmailHandler()
    {
        
    }
    
    public List<SIR> sirList = new List<SIR>();
    public List<string> eUrlList = new List<string>();
    public string eHeader { get; set; }
    public string eBody { get; set; }
    public string eSubject { get; set; }
    public string eAddress { get; set; }
    
    
    public enum NoiType
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury,
        StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftOfProperties
    }
    
    
    public override string ProcessMessage(string header, string message)
    {
        const string senderTag = "Sender:";
        const string subjectTag = "Subject:";
        const string messageTag = "Message:";
        const string sortcodeTag = "Sort Code:";
        const string noiTag = "Nature of Incident:";
        eHeader = header;

        int senderIndex = message.IndexOf(senderTag, StringComparison.OrdinalIgnoreCase);
        int subjectIndex = message.IndexOf(subjectTag, StringComparison.OrdinalIgnoreCase);
        int messageIndex = message.IndexOf(messageTag, StringComparison.OrdinalIgnoreCase);
        
        if (senderIndex == -1 || subjectIndex == -1 || messageIndex == -1)
        {
            throw new Exception("Missing required tags: Sender, Subject, or Message");
        }

        string senderValue = GetTagValue(senderTag, message, senderIndex);
        string subjectValue = GetTagValue(subjectTag, message, subjectIndex);
        string messageValue = GetTagValue(messageTag, message, messageIndex);
        

        if (!senderValue.Contains("@"))
        {
            return "Sender value must contain '@'";
        }

        if (subjectValue.Length > 20)
        {
            return "Subject value must be less than or equal to 20 characters";
        }

        if (messageValue.Length > 1028)
        {
            return "Message value must be less than or equal to 1028 characters";
        }
        
        //check for SIR
        if (subjectValue.StartsWith("SIR"))
        {
            int scIndex = message.IndexOf(sortcodeTag, StringComparison.OrdinalIgnoreCase);
            int noiIndex = message.IndexOf(noiTag, StringComparison.OrdinalIgnoreCase);
            
            bool subjectValid = ValidateSbjLineFormat(subjectValue);

                string sc = GetTagValue(sortcodeTag, message, scIndex);
                string noi = GetTagValue(noiTag, message, noiIndex);
                bool validSC =  ValidateCentreCodeFormat(sc);
                bool validNoi = IsValidNoiType(noi);
                
                if (subjectValid && validSC && validNoi)
                {
                    SIR noiObj = new SIR(sc, noi);
                    sirList.Add((noiObj));
                   
                }
                else
                {
                    throw new Exception("invalid SIR structure");
                }
            
        }

// Helper method to validate SIR subject line format
        bool ValidateSbjLineFormat(string sbjLine)
        {
            /*if (sbjLine.Length <= 12)
            {
                throw new Exception("SIR subject line must be less than 12 chars");
            }*/

            string[] dateParts = sbjLine.Substring(4).Split('/');
            if (dateParts.Length != 3 || !IsDigitsOnly(dateParts[0]) || !IsDigitsOnly(dateParts[1]) || !IsDigitsOnly(dateParts[2]))
            {
                throw new Exception("SIR subject line must be in the format of: SIR dd/mm/yy");
            }

            return true;
        }

// Helper method to validate centre code format
        bool ValidateCentreCodeFormat(string centreCode)
        {
            /*if (centreCode.Length <= 9)
            {
                throw new Exception("Centre code must be in the format of: nn-nnn-nn (n is a number).");
            }*/

            string[] codeParts = centreCode.Split('-');
            if (codeParts.Length != 3 || !IsDigitsOnly(codeParts[0]) || !IsDigitsOnly(codeParts[1]) || !IsDigitsOnly(codeParts[2]))
            {
                throw new Exception("Centre code must be in the format of: nn-nnn-nn (n is a number).");
            }

            return true;
        }

// Helper method to check if a string consists of digits only
        bool IsDigitsOnly(string input)
        {
            return input.All(char.IsDigit);
        }

        messageValue = RedactHyperlinks(messageValue);
        
        Console.WriteLine("lewis " + $"Sender: {senderValue}\nSubject: {subjectValue}\nMessage: {messageValue}");
        return $"Sender: {senderValue}\nSubject: {subjectValue}\nMessage: {messageValue}";
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

    private string RedactHyperlinks(string message)
    {
        // Pattern to match URLs
        string urlPattern = @"http[s]?:\/\/[^\s]+";

        // Replace URLs with "<URL Quarantined>"
        string redactedMessage = System.Text.RegularExpressions.Regex.Replace(
            message, urlPattern, "<URL Quarantined>");

        // Extract and store URLs in the urlList
        foreach (System.Text.RegularExpressions.Match match in 
            System.Text.RegularExpressions.Regex.Matches(message, urlPattern))
        {
            eUrlList.Add(match.Value);
        }
        
        return redactedMessage;
    }
    
    public bool IsValidNoiType(string value)
    {
        return Enum.TryParse(typeof(NoiType), value, out _);
    }
    
}