// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication.Azure
{
    using System.Collections.Generic;

    using global::Azure;
    using global::Azure.Communication.Sms;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The AzureSMSService class represents a service for sending SMS messages through Azure.
    /// </summary>
    public class AzureSMSService : ISmsService
    {
        private readonly ILogger<AzureSMSService> _logger;

        /// <summary>
        /// Constructor for the AzureSMSService class.
        /// </summary>
        /// <param name="logger">An instance of ILogger for logging events.</param>
        public AzureSMSService(ILogger<AzureSMSService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method for sending SMS messages through Azure.
        /// </summary>
        /// <param name="connectionString">The connection string to the Azure SMS service.</param>
        /// <param name="fromPhoneNumber">The sender's phone number for SMS.</param>
        /// <param name="toPhoneNumbers">An array of recipient phone numbers for SMS.</param>
        /// <param name="message">The text of the SMS message.</param>
        /// <returns>A collection of SMS sending results.</returns>
        public IEnumerable<SMSSendResult> SendSms(string connectionString, string fromPhoneNumber, string[] toPhoneNumbers, string message)
        {
            List<SMSSendResult> smsSendingResults = new List<SMSSendResult>();

            SmsClient smsClient = new SmsClient(connectionString);

            try
            {
                Response<IReadOnlyList<SmsSendResult>> response = smsClient.Send(fromPhoneNumber, toPhoneNumbers, message, new SmsSendOptions(true) { Tag = "verification" });

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
    }
}
