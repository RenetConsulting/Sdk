// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Microsoft.Extensions.Logging;

namespace Sdk.Communication.Aws1
{
    public class PhoneService(ILogger<PhoneService> logger, AmazonPinpointClient phoneClient) : IPhone
    {
        public async Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber)
        {
            logger.LogInformation("Start PhoneNumberValidate method");
            PhoneNumberValidateRequest request = new() { NumberValidateRequest = new() { PhoneNumber = phoneNumber } }; 

            var response = await phoneClient.PhoneNumberValidateAsync(request);

            PhoneNumberValidateResponse phoneNumberValidateResponse = new();
            if (response != null)
            {
                logger.LogInformation("Aws responce received");

                phoneNumberValidateResponse.Carrier = response.NumberValidateResponse.Carrier;
                phoneNumberValidateResponse.Timezone = response.NumberValidateResponse.Timezone;
                phoneNumberValidateResponse.Country = response.NumberValidateResponse.Country;
                phoneNumberValidateResponse.CountryCodeIso2 = response.NumberValidateResponse.CountryCodeIso2;
                phoneNumberValidateResponse.County = response.NumberValidateResponse.County;
                phoneNumberValidateResponse.City = response.NumberValidateResponse.City;
                phoneNumberValidateResponse.PhoneNumberE164 = response.NumberValidateResponse.CleansedPhoneNumberE164;
                phoneNumberValidateResponse.PostalCode = response.NumberValidateResponse.ZipCode;
                phoneNumberValidateResponse.PhoneType = response.NumberValidateResponse.PhoneType;
                phoneNumberValidateResponse.PhoneTypeCode = (PhoneTypeCode)response.NumberValidateResponse.PhoneTypeCode;

                logger.LogInformation("Aws Data collected");
            }

            return phoneNumberValidateResponse;
        }
    }
}
