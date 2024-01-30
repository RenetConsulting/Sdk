// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Amazon;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Sdk.Communication.Aws
{
    public partial class PhoneService(ILogger<PhoneService> logger, IOptions<AWSSmsServiceSettings> awsSmsServiceSettings) : IPhone
    {
        public async Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber)
        {
            PhoneNumberValidateResponse phoneNumberValidateResponse = new();

            PhoneNumberValidateRequest request = new() { NumberValidateRequest = new() { PhoneNumber = phoneNumber } };

            try
            {
                Regex phoneNumberRegex = PhoneValidation();

                if (!phoneNumberRegex.IsMatch(phoneNumber))
                {
                    throw new Exception($"Invalid or fake phone number: {phoneNumber}");
                }

                var regionEndpoint = RegionEndpoint.GetBySystemName(awsSmsServiceSettings.Value.REGION);

                AmazonPinpointClient amazonPinpointClient = new(awsSmsServiceSettings.Value.ACCESS_KEY_ID, awsSmsServiceSettings.Value.SECRET_ACCESS_KEY, regionEndpoint);

                logger.LogInformation("Start PhoneNumberValidate method");

                var response = await amazonPinpointClient.PhoneNumberValidateAsync(request);

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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while validating phone number: {PhoneNumber}", phoneNumber);

                throw;
            }
        }

        [GeneratedRegex(@"^(?!(211|311|411|511|611|711|811|911|988|700|950))(?:\+?1)?[2-9]\d{2}[2-9]\d{6}$", RegexOptions.Compiled)]
        private static partial Regex PhoneValidation();
    }
}
