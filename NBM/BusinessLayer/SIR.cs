namespace NBM.Filters;

public class SIR
{
    public SIR(string sortCode, string incidentType)
    {
        SortCode = sortCode;
        IncidentType = incidentType;
    }
    
    private string sortCode;
    private string incidentType;
    
    public string SortCode
    {
        get { return sortCode; }
        set { sortCode = value; }
    }

    public string IncidentType
    {
        get { return incidentType; }
        set { incidentType = value; }
    }

}