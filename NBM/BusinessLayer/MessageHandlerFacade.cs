using System.Dynamic;
using Microsoft.VisualBasic;

namespace NBM.Filters;

public class MessageHandlerFacade
{
    
    public List<string> messages = new List<string>();
    
    
   public (string messageHeader, string messageBody) GetSanitizedMessage(string messageHeader, string messageBody)
   {
        MessageFilter mv = new MessageFilter();
        
        //validate the message and create message handler of message type determined by ValidateMessage function
        var messageHandler = mv.ValidateMessage(messageHeader, messageBody);
         
       
        
        //create var cleanedMessage which is the sanitized message from ProcessMessage function and
        //add it to the messages list
        var cleanedMessage =  messageHandler.ProcessMessage(messageBody);
        messages.Add(cleanedMessage);
        
        //create var completemessage to be returned as this contains the message header and the sanitized message
        //this is because this var can then be used for the json serializer to create a json including 
        //the unique message id. 
        var completeMessage = (messageHeader, cleanedMessage);

        return completeMessage;
      
    }
    
    //new method to call function in json class to create message

    public void CreateJson(object obj)
    {
        JSONService js = new JSONService();
        js.WriteToJson(obj);
    }
    
    //end session calls this
    
    //getlists
    //call th.getlists
    //call email.gl
    
    //return lists
    
    //public List<String> GetEmailLists

}