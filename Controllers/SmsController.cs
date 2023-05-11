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
        private readonly IConfiguration _config;

        public SmsController(ILogger<SmsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        // Receive and respond to incoming sms from twilio
        [Microsoft.AspNetCore.Mvc.HttpPost("receive")]
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            string input = incomingMessage.Body;
            SmsHelper sms = new SmsHelper();
            DataAccess da = new DataAccess(_config);
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
                //da.insertMessage("From#", "To#", "MsgBody", "SmsSid");
                da.insertMessage(incomingMessage.From, incomingMessage.To, incomingMessage.Body, incomingMessage.SmsSid);
                da.insertMessage(incomingMessage.To, incomingMessage.From, responseMsg, string.Empty);
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
