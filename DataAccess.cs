
namespace WebAPI;

public class DataAccess
{
    private readonly SmsCodeSenderContext _dbContext;

    public DataAccess(SmsCodeSenderContext dbContext)
    {
        _dbContext = dbContext;

    }

    public void insertMessage(string fromPhone, string toPhone, string messageBody, string messageSid)
    {
        SmsMessage msg = new SmsMessage
        {
            FromPhone = fromPhone,
            ToPhone = toPhone,
            MessageBody = messageBody,
            MessageSid = messageSid
        };

        _dbContext.SmsMessages.Add(msg);
        _dbContext.SaveChanges();
    }

    public void insertMessages(List<SmsMessage> items)
    {
        _dbContext.AddRange(items);
        _dbContext.SaveChanges();
    }

    public IEnumerable<SmsMessage> GetLast50SmsMessages()
    {
        return _dbContext.SmsMessages.OrderByDescending(s => s.SmsMessageId).Take(50).ToList();
    }

    public int GetTotalSmsMessageCount()
    {
        return _dbContext.SmsMessages.Count();
    }
}