namespace NBM.Filters;

public abstract class MessageHandler
{
    public abstract String ProcessMessage(String header, String message);
}