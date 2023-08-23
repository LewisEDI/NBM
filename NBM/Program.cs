// See https://aka.ms/new-console-template for more information

using NBM.Filters;


string tweet = "@lewisbogle,hello AAP,#yolo,@somerandom";
//string tweet = "@lewisbogle hello AAP #yolo @somerandom";
string header = "t123456789";

MessageHandlerFacade mhf = new MessageHandlerFacade();
var obj = mhf.GetSanitizedMessage(header, tweet);
mhf.CreateJson(obj);
RunTests();


 static void RunTests()
        {
            //TestValidMessage();
            //TestValidSIR();
            /*TestInvalidSender();
            TestInvalidSubjectLength();
            TestInvalidMessageLength();
            TestValidSIRSubject();
            TestInvalidSIRSubject();*/
            //TestAddHashtagsAndMentions();
            //TestQuarantinedUrls();
            TestStoreUrls2();
        }

        static void TestValidMessage()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "e123456789";
            string sample = "Sender:test@example.com\nSubject:Test Subject\nMessage:message1";
            string result = emailHandler.ProcessMessage(header, sample);
            Console.WriteLine(result);
        }

        static void TestValidSIR()
        {
         EmailHandler emailHandler = new EmailHandler();
        string header = "e123456789";
        string sample = "Sender:test@example.com\nSubject:SIR 09/10/81\nMessage:Sort Code:99-99-99\nNature of Incident:TheftOfProperties";
        string result = emailHandler.ProcessMessage(header, sample);
        Console.WriteLine(result);
        }

        /*
        static void TestInvalidSender()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "e123456789";
            string sample = "Sender: invalid\nSubject: Test Subject\nMessage: Test Message";
            string result = emailHandler.ProcessMessage(header, sample);
            Console.WriteLine(result);
        }

        static void TestInvalidSubjectLength()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "Sender: test@example.com\nSubject: Very Long Subject That Exceeds 20 Characters\nMessage: Test Message";
            string result = emailHandler.ProcessMessage(header, "Sample message content");
            Console.WriteLine(result);
        }

        static void TestInvalidMessageLength()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "Sender: test@example.com\nSubject: Test Subject\nMessage: ";
            string longMessage = new string('X', 1030); // Create a message with length > 1028
            string result = emailHandler.ProcessMessage(header, longMessage);
            Console.WriteLine(result);
        }

        static void TestValidSIRSubject()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "Sender: test@example.com\nSubject: SIR 01/02/22\nMessage: Test Message";
            string result = emailHandler.ProcessMessage(header, "Sample message content");
            Console.WriteLine(result);
        }

        static void TestInvalidSIRSubject()
        {
            EmailHandler emailHandler = new EmailHandler();
            string header = "Sender: test@example.com\nSubject: SIR invalid\nMessage: Test Message";
            string result = emailHandler.ProcessMessage(header, "Sample message content");
            Console.WriteLine(result);
        }
        */

static void TestAddHashtagsAndMentions()
{
    TweetHandler tweetHandler = new TweetHandler();
    string header = "t123456789";
    string sample = "@user123,Hello,#important update,@john.doe";
    string result = tweetHandler.ProcessMessage(header, sample);
    Console.WriteLine("Hashtags:");
    foreach (string hashtag in tweetHandler.tHashtags)
    {
        Console.WriteLine(hashtag);
    }

    Console.WriteLine("Mentions:");
    foreach (string mention in tweetHandler.tMentions)
    {
        Console.WriteLine(mention);
    }
}

static void TestQuarantinedUrls()
{
    EmailHandler emailHandler = new EmailHandler();
    string header = "e123456789";
    string sample = "Sender:test@example.com\nSubject:Test Subject\nMessage:Check out this link: https://example.com";
    string result = emailHandler.ProcessMessage(header, sample);
            
    Console.WriteLine("Quarantined URLs:");
    foreach (string quarantinedUrl in emailHandler.eUrlList)
    {
        Console.WriteLine(quarantinedUrl);
    }
}

static void TestStoreUrls2()
{
    // Simulate email message
    string header = "e123456789";
    string messageBody = "Sender:test@example.com\nSubject:Test Subject\nMessage:Check out these links: https://example1.com, https://example2.com";

    MessageHandlerFacade mhf = new MessageHandlerFacade();
    var obj = mhf.GetSanitizedMessage(header, messageBody);

    // Call the StoreUrls2 method
    mhf.StoreUrls();

    Console.WriteLine("URLs stored successfully.");
}

