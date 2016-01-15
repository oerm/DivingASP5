using DivingApp.Models.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class PassportViewModel
    {
        private const string hourString = "h.";
        private const string minString = "min.";
        private const string missingString = "Not exist";
                

        public string Email { get; set; }

        public string Fio { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }

        public decimal CountryId { get; set; }

        public int DivesCount { get; set; }

        public int SumDiveMinutes { get; set; }

        public double MaxDepth { get; set; }

        public IEnumerable<DiveViewModel> Dives { get; set; }

        public IEnumerable<CertViewModel> Certs { get; set; }

        public string SumDiveMinutesText
        {
            get
            {
                int hours = SumDiveMinutes / 60;
                int leftMinutes = SumDiveMinutes - (hours * 60);
                return string.Format("{0} {1} {2} {3}", hours.ToString(), hourString, leftMinutes, minString);
            }
        }
        public byte MaxCertLevel
        {
            get
            {

                return Certs.Count() > 0 ? Certs.Select(cert => cert.Level).Max() : (byte)0;
            }
        }

        public string MaxCertName
        {
            get
            {
                var certName = Certs.OrderByDescending(cert => cert.Level).Select(cert => cert.CertName).FirstOrDefault();
                if (certName == null) certName = missingString;
                return certName;
            }
        }       

        public IEnumerable<CertViewModel> GetCertsBasic
        {
            get
            {
                return Certs.Where(cert => cert.IsGeneral);
            }
        }

        public IEnumerable<CertViewModel> GetCertsSecondary
        {
            get
            {
                return Certs.Where(cert => !cert.IsGeneral);
            }
        }

        public IEnumerable<GroupedCountryViewModel> diveCountries;

    }
}
