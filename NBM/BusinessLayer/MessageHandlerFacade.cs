using Microsoft.VisualBasic;

namespace NBM.Filters;

public class MessageHandlerFacade
{
    
    List<string> messages = new List<string>();
    
    (string messageHeader, string messageBody) GetSanitizedMessage(string message)
    {
        MessageValidator mH = new MessageValidator();
        var messageHandler = mH.ValidateMessage("e123456789", "This is my email");//message
        var cleanedMessage = messageHandler.ProcessMessage("a mess"); //message
        messages.Add(cleanedMessage);

        return cleanedMessage;
    }
    
    //new method to call function in json class to create message
    
}