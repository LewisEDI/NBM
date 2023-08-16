// See https://aka.ms/new-console-template for more information

using NBM.Filters;

MessageFilter filtertest = new MessageFilter();

string test = "e123456789";
string invtest = "ee99999999";

filtertest.MessageType(invtest);

string tweet = "@lewisbogle,hello,#yolo,@somerandom";

TweetHandler thandler = new TweetHandler();

thandler.ProcessMessage(tweet);

AbbreviationConverter converter = new AbbreviationConverter();

string inputString = "AAP";

converter.ConvertAbbreviations(inputString);