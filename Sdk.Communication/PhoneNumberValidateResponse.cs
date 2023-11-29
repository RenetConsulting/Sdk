// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication
{
    public class PhoneNumberValidateResponse
    {
        public string Carrier {  get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCodeIso2 { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PhoneType { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
        public string PhoneNumberE164 { get; set; } = string.Empty;
        public PhoneTypeCode PhoneTypeCode { get; set; }
    }
}
