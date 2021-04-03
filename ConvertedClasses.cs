using System;
using System.Collections.Generic;
using System.Text;

namespace CVSCovidLocator
{
     
    public class ResponseMetaData
    {
        public string statusCode { get; set; }
        public string statusDesc { get; set; }
        public string conversationID { get; set; }
        public string refId { get; set; }
    }

    public class DayHour
    {
        public string Day { get; set; }
        public string Hours { get; set; }
    }

    public class StoreHours
    {
        public List<DayHour> DayHours { get; set; }
    }

    public class PharmacyHours
    {
        public List<DayHour> DayHours { get; set; }
    }

    public class ImmunizationAvailability
    {
        public List<string> available { get; set; }
        public List<object> unavailable { get; set; }
    }

    public class ImzAdditionalData
    {
        public string imzType { get; set; }
        public List<string> availableDates { get; set; }
    }

    public class Location
    {
        public string StoreNumber { get; set; }
        public string minuteClinicID { get; set; }
        public string opticalClinicID { get; set; }
        public int storeType { get; set; }
        public string covaxInd { get; set; }
        public string pharmacyNCPDPProviderIdentifier { get; set; }
        public string addressLine { get; set; }
        public string addressCityDescriptionText { get; set; }
        public string addressState { get; set; }
        public string addressZipCode { get; set; }
        public string addressCountry { get; set; }
        public string geographicLatitudePoint { get; set; }
        public string geographicLongitudePoint { get; set; }
        public string indicatorStoreTwentyFourHoursOpen { get; set; }
        public string indicatorPrescriptionService { get; set; }
        public string indicatorPhotoCenterService { get; set; }
        public string indicatorOpticalService { get; set; }
        public string instorePickupService { get; set; }
        public string indicatorDriveThruService { get; set; }
        public string indicatorPharmacyTwentyFourHoursOpen { get; set; }
        public string rxConvertedFlag { get; set; }
        public string indicatorCircularConverted { get; set; }
        public string indicatorH1N1FluShot { get; set; }
        public string indicatorRxFluFlag { get; set; }
        public string indicatorWicService { get; set; }
        public string snapIndicator { get; set; }
        public string indicatorVaccineServiceSupport { get; set; }
        public string indicatorPneumoniaShotService { get; set; }
        public string indicatorWeeklyAd { get; set; }
        public string indicatorCVSStore { get; set; }
        public string indicatorStorePickup { get; set; }
        public string storeLocationTimeZone { get; set; }
        public string storePhonenumber { get; set; }
        public string pharmacyPhonenumber { get; set; }
        public StoreHours storeHours { get; set; }
        public PharmacyHours pharmacyHours { get; set; }
        public string adVersionCdCurrent { get; set; }
        public string adVersionCdNext { get; set; }
        public string distance { get; set; }
        public ImmunizationAvailability immunizationAvailability { get; set; }
        public string schedulerRefId { get; set; }
        public List<ImzAdditionalData> imzAdditionalData { get; set; }
        public string mfrName { get; set; }
        public string additionalDoseRequired { get; set; }
    }

    public class ResponsePayloadData
    {
        public string schedulerRefType { get; set; }
        public List<string> availableDates { get; set; }
        public List<Location> locations { get; set; }
    }

    public class Root
    {
        public ResponseMetaData responseMetaData { get; set; }
        public ResponsePayloadData responsePayloadData { get; set; }
    }


}
