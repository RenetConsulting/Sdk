// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication
{
    /// <summary>
    /// Model that display an SMS sending result.
    /// </summary>
    public class SMSSendResult
    {
        /// <summary>
        /// Message Id.
        /// </summary>
        public string MessageId { get; set; } = string.Empty;

        /// <summary>
        /// User name to receive a message.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Result of receiving a message.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// If someting goes wrong - get an error message.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
