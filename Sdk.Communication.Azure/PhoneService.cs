// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure.Communication.PhoneNumbers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Sdk.Communication.Azure
{
    /* This service is not ready for production use */
    public class PhoneService(ILogger<PhoneService> logger, PhoneNumbersClient phoneNumbersClient) : IPhone
    {
        private readonly ILogger<PhoneService> _logger = logger;
        
        // PhoneNumbersClient client = new PhoneNumbersClient(_azureCommunicationSettings.CommunicationServicesConnectionString, new PhoneNumbersClientOptions(PhoneNumbersClientOptions.ServiceVersion.V2023_05_01_Preview));
        private readonly PhoneNumbersClient _phoneNumbersClient = phoneNumbersClient;


        public async Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber)
        {
            PhoneNumberValidateResponse phoneNumberValidateResponse = new();

            OperatorInformationResult searchResult = await _phoneNumbersClient.SearchOperatorInformationAsync(new[] { phoneNumber });
            if (searchResult != null && searchResult.Values != null && searchResult.Values.Count > 0)
            {
                OperatorInformation operatorInformation = searchResult.Values[0];

                phoneNumberValidateResponse.Carrier = operatorInformation.OperatorDetails.Name;
                phoneNumberValidateResponse.PhoneType = operatorInformation.NumberType.HasValue ? operatorInformation.NumberType.Value.ToString() : string.Empty;
                phoneNumberValidateResponse.CountryCodeIso2 = operatorInformation.IsoCountryCode;
                phoneNumberValidateResponse.PhoneTypeCode = phoneNumberValidateResponse.PhoneType.ToLower() switch
                {
                    "mobile" => PhoneTypeCode.Mobile,
                    // "geographic" => PhoneTypeCode.Voip, - The "geographic" is for both VOIP and Landline
                    _ => PhoneTypeCode.Invalid,
                };
            }
            return phoneNumberValidateResponse;
        }
    }
}
