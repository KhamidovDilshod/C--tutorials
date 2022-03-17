namespace C__tutorials.DTO;
#pragma warning disable
public class Currency
{
    public int Id { get; set; }
    public string code { get; set; }
    public string title { get; set; }
    public string cb_price { get; set; }
    public string nbu_buy_price { get; set; }
    public string nbu_sell_price { get; set; }
    public string date { get; set; }

    public Currency(string code, string title, string cb_price, string nbu_buy_price, string nbu_sell_price,
        string date)
    {
        this.code = code;
        this.title = title;
        this.cb_price = cb_price;
        this.nbu_buy_price = nbu_buy_price;
        this.nbu_sell_price = nbu_sell_price;
        this.date = date;
    }

    public Currency()
    {
        
    }
}