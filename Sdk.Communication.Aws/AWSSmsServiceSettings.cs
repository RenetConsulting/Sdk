// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication.Aws
{
    public class AWSSmsServiceSettings
    {
        public required string AccessKeyId { get; set; }

        public required string AccessSecret { get; set; }

        public required string Region { get; set; }
    }
}
