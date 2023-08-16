namespace NBM.Filters;

public class EmailHandler : MessageHandler
{
    public EmailHandler()
    {
        
    }
    
    public string eBody { get; set; }
    public string eSubject { get; set; }
    public eTypes eType { get; set; }
    public eNofi nOi { get; set; }
    public string eAddress { get; set; }
    public List<string> eUrList { get; set; }
    
    public enum eTypes
    {
        Sir, Standard
    }
    
    public enum eNofi
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury, StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftofProperties
    }
    
    public override String ProcessMessage(String message)
    {
        string s = string.Empty;
        return s;
    }
}