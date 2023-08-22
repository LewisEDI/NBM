namespace NBM.Filters;

public class NatureOfIncident
{
    public NatureOfIncident(string sortCode, string incidentType)
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