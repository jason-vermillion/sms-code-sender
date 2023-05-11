namespace WebAPI;

public class SmsCodeRequestModel
{
    private readonly IConfiguration _config;
    public SmsCodeRequestModel(IConfiguration configuration)
    {
        _config = configuration;
    }

    public SmsCodeListResponseDto getCodeList(int offset, int limit)
    {
        SmsCodeListResponseDto result = new SmsCodeListResponseDto();
        DataAccess da = new DataAccess(_config);

        result.Limit = 50;
        result.Offset = 0;
        result.TotalCount = da.GetTotalSmsMessageCount();

        var messages = da.GetLast50SmsMessages();

        result.Items = new List<SmsCodeDto>();
        foreach (var sms in messages)
        {
            result.Items.Add(
                new SmsCodeDto
                {
                    FromPhone = sms.FromPhone,
                    ToPhone = sms.ToPhone,
                    MessageBody = sms.MessageBody,
                    MessageSid = sms.MessageSid,
                    CreatedOn = sms.CreatedOn
                }
            );
        }

        return result;
    }
}
