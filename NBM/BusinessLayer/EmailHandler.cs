namespace NBM.Filters;

public class EmailHandler : MessageHandler
{
    public EmailHandler()
    {
        
    }
    
    public string eHeader { get; set; }
    public string eBody { get; set; }
    public string eSubject { get; set; }
    public eTypes eType { get; set; }
    public  NoiInfo nOi { get; set; }
    public string eAddress { get; set; }
    public List<string> eUrlList { get; set; }

    public class NoiInfo
    {
        public string CentreCode { get; set; }
        public eNofi Type { get; set; }
    }
    
    public enum eTypes
    {
        Sir, Standard
    }
    
    public enum eNofi
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury, StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftofProperties
    }
    
  
    
    public override string ProcessMessage(string header, string message)
    {
        const string senderTag = "Sender:";
        const string subjectTag = "Subject:";
        const string messageTag = "Message:";

        int senderIndex = message.IndexOf(senderTag, StringComparison.OrdinalIgnoreCase);
        int subjectIndex = message.IndexOf(subjectTag, StringComparison.OrdinalIgnoreCase);
        int messageIndex = message.IndexOf(messageTag, StringComparison.OrdinalIgnoreCase);

        if (senderIndex == -1 || subjectIndex == -1 || messageIndex == -1)
        {
            return "Missing required tags: Sender, Subject, or Message";
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

        messageValue = RedactHyperlinks(messageValue);


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
    
}