using WebAPI.Dto;

namespace WebAPI.Models;

public class SmsCodeRequestModel
{
    private readonly SmsCodeSenderContext _dbContext;
    public SmsCodeRequestModel(SmsCodeSenderContext dbContext)
    {
        _dbContext = dbContext;
    }

    public SmsCodeListResponseDto getCodeList(int offset, int limit)
    {
        SmsCodeListResponseDto result = new SmsCodeListResponseDto();
        DataAccess da = new DataAccess(_dbContext);

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
                    SmsMessageId = sms.SmsMessageId,
                    FromPhone = sms.ToPhone,
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
