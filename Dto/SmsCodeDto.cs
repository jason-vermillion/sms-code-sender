namespace WebAPI.Dto;

public class SmsCodeDto
{
    public int SmsMessageId { get; set; }

    public string? ToPhone { get; set; }

    public string? FromPhone { get; set; }

    public string? MessageBody { get; set; }

    public string? MessageSid { get; set; }

    public DateTime? CreatedOn { get; set; }
}
