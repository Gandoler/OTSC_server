namespace ClassLibrary1.tools.SendMailWithcode.RequestTemplates;

public class ChangePassResponse
{
    public bool ExistanceStatus { get; set; }
    public string Code { get; set; } = string.Empty;

    public ChangePassResponse(bool existanceStatus)
    {
        ExistanceStatus = existanceStatus;
    }

    public ChangePassResponse(string code)
    {
        ExistanceStatus = true;
        Code = code;
    }
}