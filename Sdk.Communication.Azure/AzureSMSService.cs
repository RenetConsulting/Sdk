// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication.Azure
{
    using System.Collections.Generic;

    using global::Azure;
    using global::Azure.Communication.Sms;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The AzureSMSService class represents a service for sending SMS messages through Azure.
    /// </summary>
    public class AzureSMSService : ISmsService
    {
        private readonly ILogger<AzureSMSService> _logger;
        private readonly AzureSmsServiceSettings _azureSmsServiceSettings;

        /// <summary>
        /// Constructor for the AzureSMSService class.
        /// </summary>
        /// <param name="logger">An instance of ILogger for logging events.</param>
        /// <param name="azureSmsServiceSettings">An settings class to setup azure sms sending data.</param>
        public AzureSMSService(ILogger<AzureSMSService> logger, IOptions<AzureSmsServiceSettings> azureSmsServiceSettings)
        {
            _logger = logger;
            _azureSmsServiceSettings = azureSmsServiceSettings.Value;
        }

        /// <summary>
        /// Method for sending an array of SMS messages through Azure.
        /// </summary>
        /// <param name="toPhoneNumbers">An array of recipient phone numbers for SMS.</param>
        /// <param name="message">The text of the SMS message.</param>
        /// <returns>A collection of SMS sending results.</returns>
        public IEnumerable<SMSSendResult> SendSms(string[] toPhoneNumbers, string message)
        {
            List<SMSSendResult> smsSendingResults = new List<SMSSendResult>();

            SmsClient smsClient = new SmsClient(_azureSmsServiceSettings.ConnectionString);

            try
            {
                Response<IReadOnlyList<SmsSendResult>> response = smsClient.Send(_azureSmsServiceSettings.FromPhoneNumber, toPhoneNumbers, message, new SmsSendOptions(true) { Tag = "verification" });

                IEnumerable<SmsSendResult> smsSendResults = response.Value;

                foreach (var result in smsSendResults)
                {
                    var sendingResult = new SMSSendResult
                    {
                        MessageId = result.MessageId,
                        CustomerName = result.To,
                        Result = result.Successful,
                        ErrorMessage = result.ErrorMessage
                    };

                    smsSendingResults.Add(sendingResult);
                }

                return smsSendingResults;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Method for sending one SMS messages through Azure.
        /// </summary>
        /// <param name="toPhoneNumbers">An array of recipient phone numbers for SMS.</param>
        /// <param name="message">The text of the SMS message.</param>
        /// <returns>SMS sending result.</returns>
        public SMSSendResult SendSms(string toPhoneNumbers, string message)
        {
            SmsClient smsClient = new SmsClient(_azureSmsServiceSettings.ConnectionString);

            try
            {
                SmsSendResult response = smsClient.Send(_azureSmsServiceSettings.FromPhoneNumber, toPhoneNumbers, message, new SmsSendOptions(true) { Tag = "verification" });

                var sendingResult = new SMSSendResult
                {
                    MessageId = response.MessageId,
                    CustomerName = response.To,
                    Result = response.Successful,
                    ErrorMessage = response.ErrorMessage
                };

                return sendingResult;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);

                throw;
            }
        }
    }
}
