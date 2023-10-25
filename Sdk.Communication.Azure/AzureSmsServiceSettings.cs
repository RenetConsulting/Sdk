// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication.Azure
{
    /// <summary>
    /// Provides settings for the Azure SMS Service.
    /// </summary>
    public class AzureSmsServiceSettings
    {
        /// <summary>
        /// Gets or sets the connection string for Azure Communication Services.
        /// </summary>
        /// <value>The connection string.</value>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the phone number from which SMS messages will be sent.
        /// </summary>
        /// <value>The phone number in E.164 format.</value>
        public string? FromPhoneNumber { get; set; }
    }

}
