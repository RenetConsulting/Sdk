// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure;

namespace Sdk.Communication.Azure
{
    public class AzureCommunicationSettings
    {
        public string CommunicationServicesConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Azure UserAssigned ClientId.
        /// </summary>
        public string UserAssignedClientId { get; set; } = string.Empty;

        public bool WaitUntilCompleted { get; set; } = false;
    }
}
