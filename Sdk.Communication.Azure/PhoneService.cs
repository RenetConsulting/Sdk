// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure.Communication.PhoneNumbers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Sdk.Communication.Azure
{
    /* This service is not ready for production use */
    public class PhoneService(ILogger<PhoneService> logger, PhoneNumbersClient phoneNumbersClient) : IPhone
    {
        public async Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber)
        {
            PhoneNumberValidateResponse phoneNumberValidateResponse = new();

            try
            {
                OperatorInformationResult searchResult = await phoneNumbersClient.SearchOperatorInformationAsync(new[] { phoneNumber });

                if (searchResult != null && searchResult.Values != null && searchResult.Values.Count > 0)
                {
                    OperatorInformation operatorInformation = searchResult.Values[0];

                    if (operatorInformation != null && operatorInformation.OperatorDetails != null)
                    {
                        phoneNumberValidateResponse.Carrier = operatorInformation.OperatorDetails.Name;
                        phoneNumberValidateResponse.CountryCodeIso2 = operatorInformation.IsoCountryCode;

                        if (operatorInformation.NumberType.HasValue)
                        {
                            string phoneType = operatorInformation.NumberType.Value.ToString();
                            phoneNumberValidateResponse.PhoneType = phoneType;
                            phoneNumberValidateResponse.PhoneTypeCode = phoneType.ToLower() switch
                            {
                                "mobile" => PhoneTypeCode.Mobile,
                                _ => PhoneTypeCode.Invalid,
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while validating phone number: {PhoneNumber}", phoneNumber);
            }

            return phoneNumberValidateResponse;
        }
    }

}
