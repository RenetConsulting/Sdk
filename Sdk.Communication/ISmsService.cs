// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using System.Collections.Generic;

namespace Sdk.Communication
{
    public interface ISmsService
    {
        IEnumerable<SMSSendResult> SendSms(string connectionString, string fromPhoneNumber, string[] toPhoneNumbers, string message);
    }
}
