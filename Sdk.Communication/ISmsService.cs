// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using System.Collections.Generic;

namespace Sdk.Communication
{
    public interface ISmsService
    {
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
        IEnumerable<SMSSendResult> SendSms(string[] toPhoneNumbers, string message, string fromPhoneNumber);

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
        public SMSSendResult SendSms(string toPhoneNumbers, string message, string fromPhoneNumber);
    }
}
