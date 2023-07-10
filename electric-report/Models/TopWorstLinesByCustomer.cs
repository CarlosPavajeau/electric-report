namespace electric_report.Models;

public class TopWorstLinesByCustomer
{
    public string CustomerType { get; set; }
    public string Line { get; set; }
    public decimal TotalLoss { get; set; }
}
