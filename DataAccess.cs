
namespace WebAPI;

public class DataAccess
{
    private readonly IConfiguration _config;

    public DataAccess(IConfiguration configuration)
    {
        _config = configuration;
    }

    public void insertMessage(string fromPhone, string toPhone, string messageBody, string messageSid)
    {
        using (SmsCodeSenderContext db = new SmsCodeSenderContext(_config))
        {
            SmsMessage msg = new SmsMessage();

            msg.FromPhone = fromPhone;
            msg.ToPhone = toPhone;
            msg.MessageBody = messageBody;
            msg.MessageSid = messageSid;

            db.SmsMessages.Add(msg);
            db.SaveChanges();
        }
    }

    public IEnumerable<SmsMessage> GetLast50SmsMessages()
    {
        using (SmsCodeSenderContext db = new SmsCodeSenderContext(_config))
        {
            return db.SmsMessages.OrderByDescending(s => s.SmsMessageId).Take(50).ToList();
        }
    }

    public int GetTotalSmsMessageCount()
    {
        using (SmsCodeSenderContext db = new SmsCodeSenderContext(_config))
        {
            return db.SmsMessages.Count();
        }
    }
}