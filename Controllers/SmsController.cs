using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using WebAPI;

namespace TwilioReceive.Controllers
{
    [Route("sms")]
    public class SmsController : TwilioController
    {
        private readonly ILogger<SmsController> _logger;
        private readonly SmsCodeSenderContext _dbContext;

        public SmsController(ILogger<SmsController> logger, SmsCodeSenderContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Receive and respond to incoming sms from twilio
        [Microsoft.AspNetCore.Mvc.HttpPost("receive")]
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            string input = incomingMessage.Body;
            SmsHelper sms = new SmsHelper();
            DataAccess da = new DataAccess(_dbContext);
            string responseMsg = "Invalid message, expected a number";

            // validate
            if (!string.IsNullOrEmpty(input) && sms.isNumericString(input))
            {
                responseMsg = sms.GetRandomNumericString(6);
            }

            // respond back with an sms message containing the new number.
            messagingResponse.Message(responseMsg);

            try
            {
                List<SmsMessage> messages = new List<SmsMessage>();
                messages.Add(
                    new SmsMessage
                    {
                        FromPhone = incomingMessage.From,
                        ToPhone = incomingMessage.To,
                        MessageBody = incomingMessage.Body,
                        MessageSid = incomingMessage.MessageSid
                    }
                );

                messages.Add(
                    new SmsMessage
                    {
                        FromPhone = incomingMessage.To,
                        ToPhone = incomingMessage.From,
                        MessageBody = incomingMessage.Body,
                        MessageSid = String.Empty
                    }
                );

                da.insertMessages(messages);
            }
            catch (Exception e)
            {
                // log exception.
                _logger.LogError(e, "EF error in SmsController.Index()");
            }

            return TwiML(messagingResponse);
        }

    }
}
