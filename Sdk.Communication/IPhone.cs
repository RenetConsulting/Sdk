// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Communication
{
    public interface IPhone
    {
        Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber);
    }
}
