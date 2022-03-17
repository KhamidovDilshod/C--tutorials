namespace C__tutorials.DTO;

public class BankResponse
{
    public BankResponse(string message, bool success, string role, string token, string userName, int statusCode)
    {
        this.Message = message;
        this.IsSuccess = success;
        this.Role = role;
        this.Token = token;
        this.UserName = userName;
        this.statusCode = statusCode;
    }

    public BankResponse()
    {
        
    }

    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public int statusCode { get; set; }
}