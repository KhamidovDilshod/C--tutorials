using Newtonsoft.Json;

namespace C__tutorials.Models;
#pragma warning disable
public class ClientDetails
{
    public int Id { get; set; }
    public string RegisterId { get; set; }
    public string Branch { get; set; }
    public string Client { get; set; }
    public string tin { get; set; }
    public double CurrencyAmount { get; set; }
    public double Amount { get; set; }
    public double Remaining { get; set; }
    public string Currency { get; set; }
    public string ContractNumber { get; set; }
    public string OperDate { get; set; }
    public string Status { get; set; }
    public string OddType { get; set; }
    public string LastUsed { get; set; }
    public string Comments { get; set; }
    public string CurrencyCourse { get; set; }
}