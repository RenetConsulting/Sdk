// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure.Communication.Email;

namespace System.Net.Mail
{
    public static class MailAddressExtension 
    {
        public static EmailAddress ToEmailAddress(this MailAddress value) {
            return new EmailAddress(value.Address, value.DisplayName);
        }
    }
}
