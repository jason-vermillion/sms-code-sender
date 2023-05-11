namespace WebAPI;

// response body DTO format for the /api/
public class SmsCodeListResponseDto
{
    public List<SmsCodeDto>? Items { get; set; }

    public int TotalCount { get; set; }

    public int Limit { get; set; }

    public int Offset { get; set; }
}