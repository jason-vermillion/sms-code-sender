using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebAPI;

public class SmsHelper
{
    public void SendMessage(string to, string from, string msgBody, string accountSid, string authToken)
    {
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
            new PhoneNumber(to));
        messageOptions.From = new PhoneNumber(from);
        messageOptions.Body = "test 555 from twilio ";

        var message = MessageResource.Create(messageOptions);
    }

    public bool isNumericString(string val)
    {
        bool isNumeric = false;

        // Matches a string that is entirely numeric characters.
        isNumeric = !string.IsNullOrEmpty(val) && Regex.IsMatch(val, @"^\d+$");

        return isNumeric;
    }

    public string GetRandomNumericString(int length)
    {
        // set of possible characters in the random string.
        string[] chars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string result = string.Empty;
        Random r = new Random();
        int n = chars.Length;

        for (int i = 0; i < length; ++i)
        {
            int val = r.Next(0, n);
            result += chars[val];
        }

        return result;
    }

}