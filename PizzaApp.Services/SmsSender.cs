using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PizzaApp.Services
{
    public class SmsSender
    {
        const string accountSid = "AC36e9eea5cdb83fc53e3d062329e9cf8a";
        const string authToken = "5f9ae5072cc2de5725849eb80911bd08";
        const string MY_NUMBER = "+19106000928";

        public string SendSms(string number)
        {
            TwilioClient.Init(accountSid, authToken);
            Random random = new Random();

            string code = string.Empty;

            for (int i = 0; i < 4; i++) {
                code += random.Next(10).ToString();
            } 

            var message = MessageResource.Create(
            body: code,
            from: new Twilio.Types.PhoneNumber(MY_NUMBER),
            to: new Twilio.Types.PhoneNumber(number)
            );

            return code;
        }
    }
}
