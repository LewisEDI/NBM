// See https://aka.ms/new-console-template for more information

using NBM.Filters;


string tweet = "@lewisbogle,hello AAP,#yolo,@somerandom";
//string tweet = "@lewisbogle hello AAP #yolo @somerandom";
string header = "t123456789";

MessageHandlerFacade mhf = new MessageHandlerFacade();
var obj = mhf.GetSanitizedMessage(header, tweet);
mhf.Create(obj);




