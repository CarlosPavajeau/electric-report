namespace electric_report.Models;

public class ConsumptionHistoryByCustomerType
{
    public string CustomerType { get; set; }
    public string Line { get; set; }
    public DateTime Date { get; set; }
    public decimal Consumption { get; set; }
    public decimal Loss { get; set; }
    public decimal Cost { get; set; }
}
