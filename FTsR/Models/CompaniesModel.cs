namespace FTsR.Models;
public class Companies
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Branch { get; set; }
}

public class CompaniesForGraph
{
    public string Type { get; set; }
    public string Branch { get; set; }
}

