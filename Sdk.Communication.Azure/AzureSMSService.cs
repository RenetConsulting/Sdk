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
        private readonly SmsClient _client;


        /// <summary>
        /// Initializes a new instance of the AzureSMSService class.
        /// </summary>
        /// <param name="logger">The logger for capturing logs.</param>
        /// <param name="smsClient">The client for sending SMS.</param>
        public AzureSMSService(ILogger<AzureSMSService> logger, SmsClient smsClient)
        {
            _logger = logger;
            _client = smsClient;
        }


        /// <summary>
        /// Sends SMS to multiple phone numbers from a specified number.
        /// </summary>
        /// <param name="toPhoneNumbers">An array of phone numbers to which the SMS will be sent.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="fromPhoneNumber">The phone number from which the SMS will be sent.</param>
        /// <returns>
        /// Returns an IEnumerable of SMSSendResult objects, each containing the following information:
        /// - MessageId: The ID of the sent message.
        /// - CustomerName: The phone number of the recipient.
        /// - Result: True if the message was sent successfully; otherwise, false.
        /// - ErrorMessage: An error message if the sending of the message failed; otherwise, null.
        /// </returns>
        public IEnumerable<SMSSendResult> SendSms(string[] toPhoneNumbers, string message, string fromPhoneNumber)
        {
            List<SMSSendResult> smsSendingResults = new List<SMSSendResult>();

            try
            {
                Response<IReadOnlyList<SmsSendResult>> response = _client.Send(fromPhoneNumber, toPhoneNumbers, message, new SmsSendOptions(true) { Tag = "verification" });

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
        /// Sends an SMS message to the specified phone numbers.
        /// </summary>
        /// <param name="toPhoneNumbers">The phone numbers to send the SMS message to.</param>
        /// <param name="message">The content of the SMS message.</param>
        /// <param name="fromPhoneNumber">The phone number from which the SMS message will be sent.</param>
        /// <returns>
        /// <see cref="SMSSendResult"/> object containing the result of the SMS send operation.
        /// The <see cref="SMSSendResult.MessageId"/> property contains the ID of the sent message.
        /// The <see cref="SMSSendResult.CustomerName"/> property contains the name of the SMS message recipient.
        /// The <see cref="SMSSendResult.Result"/> property indicates whether the send operation was successful.
        /// The <see cref="SMSSendResult.ErrorMessage"/> property contains any error message in case the send operation failed.
        /// </returns>
        /// <exception cref="System.Exception">Thrown when an error occurs while sending the SMS message.</exception>
        public SMSSendResult SendSms(string toPhoneNumbers, string message, string fromPhoneNumber)
        {
            try
            {
                SmsSendResult response = _client.Send(fromPhoneNumber, toPhoneNumbers, message, new SmsSendOptions(true) { Tag = "verification" });

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
