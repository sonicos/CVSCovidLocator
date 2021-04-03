using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVSCovidLocator
{
    class Availability
    {
        public string StoreNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public decimal Distance { get; set; }
        public List<string> Dates { get; set; }
        public string Manufacturer { get; set; }
        public string SearchZip { get; set; }

        public Availability(Location loc, string searchZip)
        {
            StoreNumber = loc.StoreNumber;
            Address = loc.addressLine;
            City = loc.addressCityDescriptionText;
            State = loc.addressState;
            ZIP = loc.addressZipCode;
            Distance = decimal.Parse(loc.distance);
            Manufacturer = loc.mfrName;
            Dates = loc.imzAdditionalData.First(x => x.imzType == "CVD" && x.availableDates.Count > 0).availableDates;
            SearchZip = searchZip;
        }
    }
}
