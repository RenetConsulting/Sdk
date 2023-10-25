// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using System.Collections.Generic;

namespace Sdk.Communication
{
    public interface ISmsService
    {
        /// <summary>
        /// Method for sending an array of SMS messages through Azure.
        /// </summary>
        /// <param name="toPhoneNumbers">An array of recipient phone numbers for SMS.</param>
        /// <param name="message">The text of the SMS message.</param>
        /// <returns>A collection of SMS sending results.</returns>
        IEnumerable<SMSSendResult> SendSms(string[] toPhoneNumbers, string message);

        /// <summary>
        /// Method for sending one SMS messages through Azure.
        /// </summary>
        /// <param name="toPhoneNumbers">An array of recipient phone numbers for SMS.</param>
        /// <param name="message">The text of the SMS message.</param>
        /// <returns>SMS sending result.</returns>
        public SMSSendResult SendSms(string toPhoneNumbers, string message);
    }
}
