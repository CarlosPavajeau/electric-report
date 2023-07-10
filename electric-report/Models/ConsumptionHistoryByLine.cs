namespace electric_report.Models;

public class ConsumptionHistoryByLine
{
    public string Line { get; set; }
    public DateTime Date { get; set; }
    public decimal Consumption { get; set; }
    public decimal Loss { get; set; }
    public decimal Cost { get; set; }
}
