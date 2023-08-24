using System.Dynamic;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace NBM.Filters;

public class MessageHandlerFacade
{
    //use json 
    public List<string> messages = new List<string>();
    MessageFilter mf = new MessageFilter();
    
    
   public (string messageHeader, string messageBody) GetSanitizedMessage(string messageHeader, string messageBody)
   {
        //MessageFilter mv = new MessageFilter();
        
        //validate the message and create message handler of message type determined by ValidateMessage function
        var messageHandler = mf.ValidateMessage(messageHeader, messageBody);
         
       
        
        //create var cleanedMessage which is the sanitized message from ProcessMessage function and
        //add it to the messages list
        var cleanedMessage =  messageHandler.ProcessMessage(messageHeader, messageBody);
        messages.Add(cleanedMessage);
        
        //create var complete message to be returned as this contains the message header and the sanitized message
        //this is because this var can then be used for the json serializer to create a json including 
        //the unique message id. 
        var completeMessage = (messageHeader, cleanedMessage);

        string message = completeMessage.ToString();
        messages.Add(message);

        return completeMessage;
        //each time
      
    }
    
    //new method to call function in json class to create message

    public void CreateJson(object obj)
    {
        JSONService js = new JSONService();
        js.WriteToJson(obj);
    }
    
    //end session calls this

    public List<SIR> GetSir()
    {
        var emailObj = mf.GetEH();
        return emailObj.sirList;
    }

    public List<string> GetUrls()
    {
        var emailObj = mf.GetEH();
        return emailObj.eUrlList;
    }

    public List<string> GetHashtags()
    {
        var twitterobj = mf.GetTH();
        return twitterobj.tHashtags;
    }
    
    public List<string> GetMentions()
    {
        var twitterobj = mf.GetTH();
        return twitterobj.tMentions;
    }
    
    //getlists
    //call th.getlists
    //call email.gl
    
    //return lists
    
    //public List<String> GetEmailLists

    public void addMessages()
    {
        
    }
    
    
    public void StoreUrls()
    {
        var ehh = mf.GetEH();
        if (ehh.eUrlList.Count != 0)
        {
            Console.WriteLine("count is not 0");
            TextWriter tw = new StreamWriter(@"../../../" + "QL" + ehh.eHeader + ".txt");

            foreach (string str in ehh.eUrlList)
            {
                tw.WriteLine(str);
            }

            tw.Close();
        }
    }

}