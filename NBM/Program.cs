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
            TestValidMessage();
            TestValidSIR();
            /*TestInvalidSender();
            TestInvalidSubjectLength();
            TestInvalidMessageLength();
            TestValidSIRSubject();
            TestInvalidSIRSubject();*/
            // Add more test cases here
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
    


