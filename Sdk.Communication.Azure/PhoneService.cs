// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication.Azure
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using global::Azure.Communication.PhoneNumbers;
    using Microsoft.Extensions.Logging;

    /* This service is not ready for production use */
    public partial class PhoneService(ILogger<PhoneService> logger, PhoneNumbersClient phoneNumbersClient) : IPhone
    {
        public async Task<PhoneNumberValidateResponse> PhoneNumberValidateAsync(string phoneNumber)
        {
            Regex phoneNumberRegex = PhoneValidation();

            PhoneNumberValidateResponse phoneNumberValidateResponse = new();

            if (!phoneNumberRegex.IsMatch(phoneNumber))
            {
                logger.LogWarning("Invalid or fake phone number: {PhoneNumber}", phoneNumber);

                return phoneNumberValidateResponse;
            }

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

        [GeneratedRegex(@"^(?!(211|311|411|511|611|711|811|911|988|700|950))(?:\+?1)?[2-9]\d{2}[2-9]\d{6}$", RegexOptions.Compiled)]
        private static partial Regex PhoneValidation();
    }
}
